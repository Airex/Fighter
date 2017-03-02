using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpNeat.Core;
using SharpNeat.Domains;
using SharpNeat.Domains.PreyCapture;
using SharpNeat.Genomes.Neat;
using SharpNeat.Phenomes;

namespace MyFighterExperiment
{
    public partial class ComboView : AbstractDomainView
    {
        private readonly FightArena _arena;
        // View painting consts & objects.
      
        IGenomeDecoder<NeatGenome, IBlackBox> _genomeDecoder;
        
        /// <summary>
        /// The agent used by the simulation thread.
        /// </summary>
        IBlackBox _agent;
       
        bool _initializing = true;
        /// <summary>
        /// Thread for running simulation.
        /// </summary>
        Thread _simThread;
        /// <summary>
        /// Indicates is a simulation is running. Access is thread synchronised using Interlocked.
        /// </summary>
        int _simRunningFlag = 0;
        /// <summary>
        /// Event that signals simulation thread to start a simulation.
        /// </summary>
        AutoResetEvent _simStartEvent = new AutoResetEvent(false);

        #region Constructor

        /// <summary>
        /// Construct the view with an appropriately configured world and a genome decoder for decoding genomes as they are passed into RefreshView().
        /// </summary>
        public ComboView(IGenomeDecoder<NeatGenome, IBlackBox> genomeDecoder, FightArena arena)
        {
            _arena = arena;
            try
            {
                InitializeComponent();

                _genomeDecoder = genomeDecoder;
                

              
                // Create background thread for running simulation alongside NEAT algorithm.
                _simThread = new Thread(new ThreadStart(SimulationThread));
                _simThread.IsBackground = true;
                _simThread.Start();
            }
            finally
            {
                _initializing = false;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Refresh/update the view with the provided genome.
        /// </summary>
        public override void RefreshView(object genome)
        {
            // Zero indicates that the simulation is not currently running.
            if (0 == Interlocked.Exchange(ref _simRunningFlag, 1))
            {
                // We got the lock. Decode the genome and store result in an instance field.
                NeatGenome neatGenome = genome as NeatGenome;
                _agent = _genomeDecoder.Decode(neatGenome);

                // Signal simulation thread to start running a simulation.
                _simStartEvent.Set();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Simulate prey capture until thread is terminated.
        /// </summary>
        private void SimulationThread()
        {
            try
            {
                // Wait for first agent to be passed in.
                _simStartEvent.WaitOne();
                for (;;)
                {

                    try
                    {
                        RunTrial();
                    }
                    finally
                    {   // Simulation completed. Reset _simRunningFlag to allow another simulation to be started.
                        Interlocked.Exchange(ref _simRunningFlag, 0);
                    }
                    Thread.Sleep(6000);
                }
            }
            catch (ThreadAbortException)
            {   // Thread abort exceptions are expected.
            }
        }

        /// <summary>
        /// Run a single prey capture trial.
        /// </summary>
        private void RunTrial()
        {
            // Get local copy of agent so that the same agent is used throughout each individual simulation trial/run 
            // (_agent is being continually updated by the evolution algorithm update events). This is probably an atomic
            // operation and thus thread safe.
            IBlackBox agent = _agent;

            agent.ResetState();
            _arena.Start(agent);

            Invoke(new MethodInvoker(delegate ()
            {
                listView1.BeginUpdate();
                listView1.Items.Clear();
                foreach (var hit in _arena.Hits)
                {
                    listView1.Items.Add(hit.ToString());
                }
                listView1.EndUpdate();
            }));

          
        }

      

        #endregion

      

        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Stop the simulation thread. Otherwise painting requests to the dead control will throw an exception.
            if (null != _simThread)
            {
                _simThread.Abort();
            }
            base.OnHandleDestroyed(e);
        }

     
    }
}
