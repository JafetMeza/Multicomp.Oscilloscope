using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multicomp.Oscilloscope.Enums;
using System;
using System.Threading.Tasks;

namespace Multicomp.Oscilloscope.Test.Framework
{
    [TestClass]
    public class UnitTest1
    {

        private async Task ConfigOscilloscope(Oscilloscope oscilloscope)
        {
            await oscilloscope.Reset();
            await oscilloscope.SetChannelStatus(Source.CHAN2, Status.OFF);
            await oscilloscope.SetChannelCoupling(Source.CHAN1, Coupling.DC);
            await oscilloscope.SetChannelInverted(Source.CHAN1, Status.OFF);
            await oscilloscope.SetChanelProbe(Source.CHAN1, "1X");
            await oscilloscope.SetChannelScale(Source.CHAN1, "1V");
            await oscilloscope.SetChannelOffset(Source.CHAN1, -4);
            await oscilloscope.SetTimebaseScale("100ms");
            await oscilloscope.SetAcquireMode(AcqMode.PEAK);
            await oscilloscope.SetTriggerType();
            await oscilloscope.SetTriggerSingleSweep(TriggerSingleSweep.NORM);
            await oscilloscope.SetTriggerSingleEdgeLevel("7V");
            await oscilloscope.Stop();
            await oscilloscope.Run();
            await oscilloscope.SetTriggerSingleSweep(TriggerSingleSweep.SING);
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            Oscilloscope oscilloscope = new Oscilloscope("172.31.1.120", 3000);
            await ConfigOscilloscope(oscilloscope);
            var status = await oscilloscope.GetTriggerStatus();
            status = await oscilloscope.GetRunState();
        }
    }
}
