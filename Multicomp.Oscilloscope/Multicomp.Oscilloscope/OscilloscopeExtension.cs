
using Multicomp.Oscilloscope.Enums;
using System.Threading.Tasks;

namespace Multicomp.Oscilloscope
{
    public static class OscilloscopeExtension
    {
        #region HANDLE OSCILOSCOPE
        public static async Task Reset(this Oscilloscope osc) => await osc.WriteRawCommand("*RST");
        public static async Task Run(this Oscilloscope osc) => await osc.WriteRawCommand($":RUN RUN");
        public static async Task Stop(this Oscilloscope osc) => await osc.WriteRawCommand($":RUN STOP");
        public static async Task Auto(this Oscilloscope osc) => await osc.WriteRawCommand($":RUN AUTO");
        public static async Task DisplayClear(this Oscilloscope osc) => await osc.WriteRawCommand($":DISPLAY:CLEAR");
        public static async Task<string> GetRunState(this Oscilloscope osc) => await osc.GetDataRawCommand(":RUN?");
        #endregion

        #region GET METHODS
        public static async Task<string> GetChannelScale(this Oscilloscope osc, Source channel) => await osc.GetDataRawCommand($":CH{(int)channel}:SCAL?");
        public static async Task<string> GetTimebaseScale(this Oscilloscope osc) => await osc.GetDataRawCommand($":HORI:SCALE?");
        public static async Task<string> GetChannelOffset(this Oscilloscope osc, Source channel) => await osc.GetDataRawCommand($":CH{(int)channel}:OFFS?");
        public static async Task<string> GetLogicAnalyzerDigitalPosition(this Oscilloscope osc, Source channel) => await osc.GetDataRawCommand($":DIG{(int)channel}:POS?");
        public static async Task<string> GetChannelStatus(this Oscilloscope osc, Source channel) => await osc.GetDataRawCommand($":CH{channel}:DISP?");
        public static async Task<string> GetAcquireMode(this Oscilloscope osc) => await osc.GetDataRawCommand(":ACQ:MODE?");
        #endregion

        #region CONFIG OSCILOSCOPE
        public static async Task SetChannelCoupling(this Oscilloscope osc, Source channel, Coupling coupling) => await osc.WriteRawCommand($":CH{(int)channel}:COUP {coupling}");
        public static async Task SetChannelInverted(this Oscilloscope osc, Source channel, Status status) => await osc.WriteRawCommand($":CH{(int)channel}:INVE {status}");
        public static async Task SetChannelScale(this Oscilloscope osc, Source channel, string scale) => await osc.WriteRawCommand($":CH{(int)channel}:SCAL {scale}");
        public static async Task SetChannelOffset(this Oscilloscope osc, Source channel, int value) => await osc.WriteRawCommand($":CH{(int)channel}:OFFS {value}");
        public static async Task SetTimebaseScale(this Oscilloscope osc, string scale) => await osc.WriteRawCommand($":HORI:SCALE {scale}");
        public static async Task SetChannelStatus(this Oscilloscope osc, Source channel, Status status) => await osc.WriteRawCommand($":CH{(int)channel}:DISP {status}");
        public static async Task SetLogicAnalyzerDigitalPosition(this Oscilloscope osc, Source channel, int position) => await osc.WriteRawCommand($":DIG{(int)channel}:POS {position}");
        public static async Task SetAcquireMode(this Oscilloscope osc, AcqMode mode) => await osc.WriteRawCommand($":ACQUIRE:MODE {mode}");
        public static async Task SetChanelProbe(this Oscilloscope osc, Source channel, string probe) => await osc.WriteRawCommand($":CH{(int)channel}:PROBE {probe}");
        #endregion

        #region Triggers
        public static async Task<string> GetTriggerStatus(this Oscilloscope osc) => await osc.GetDataRawCommand(":TRIGGER:STATUS?");
        public static async Task SetTriggerType(this Oscilloscope osc) => await osc.WriteRawCommand(":TRIG:TYPE SING");
        public static async Task SetTriggerSingleSweep(this Oscilloscope osc, TriggerSingleSweep triggerSingleSweep) => await osc.WriteRawCommand($":TRIG:SINGLE:SWEEP {triggerSingleSweep}");
        public static async Task SetTriggerSingleEdgeLevel(this Oscilloscope osc, string divs) => await osc.WriteRawCommand($":TRIG:SINGLE:EDGE:LEV {divs}");
        #endregion

        #region MEASUREMENT
        public static async Task SetMeasureDisplay(this Oscilloscope osc, Status status) => await osc.WriteRawCommand($":MEASU:DISPLAY {status}");
        public static async Task<string> MeasureVpp(this Oscilloscope osc, Source channel) => await osc.GetDataRawCommand($":MEASU:CH{(int)channel}:PKPK?");
        public static async Task<string> MeasureVmax(this Oscilloscope osc, Source channel) => await osc.GetDataRawCommand($":MEASU:CH{(int)channel}:MAX?");
        public static async Task<string> MeasureVmin(this Oscilloscope osc, Source channel) => await osc.GetDataRawCommand($":MEASU:CH{(int)channel}:MIN?");
        #endregion
    }
}