﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils.Authorization
{
    public class ReadOnlyData
    {
        public static string privateKey = "PAA/AHgAbQBsACAAdgBlAHIAcwBpAG8AbgA9ACIAMQAuADAAIgAgAGUAbgBjAG8AZABpAG4AZwA9ACIAdQB0AGYALQAxADYAIgA/AD4ADQAKADwAUgBTAEEAUABhAHIAYQBtAGUAdABlAHIAcwAgAHgAbQBsAG4AcwA6AHgAcwBpAD0AIgBoAHQAdABwADoALwAvAHcAdwB3AC4AdwAzAC4AbwByAGcALwAyADAAMAAxAC8AWABNAEwAUwBjAGgAZQBtAGEALQBpAG4AcwB0AGEAbgBjAGUAIgAgAHgAbQBsAG4AcwA6AHgAcwBkAD0AIgBoAHQAdABwADoALwAvAHcAdwB3AC4AdwAzAC4AbwByAGcALwAyADAAMAAxAC8AWABNAEwAUwBjAGgAZQBtAGEAIgA+AA0ACgAgACAAPABFAHgAcABvAG4AZQBuAHQAPgBBAFEAQQBCADwALwBFAHgAcABvAG4AZQBuAHQAPgANAAoAIAAgADwATQBvAGQAdQBsAHUAcwA+AG8AYQBmAHIAMgAyAGUAZgBJAFIAVQBIAG4AUQBqAEIAdABqAFkAVQA3AGgAWQBoAFUALwAvAFQAQgBTAEIAdwBlAEgARwBhAFcAagBsAEcAdwB2AGIAYgBNAHEAaABBAEQAOABmAEUAbQBUAGgAdwB4ADEAcABSADgAVABKAGUAdQBLAFAASQBOAE8AVwBlAGYAUAAyAEYAUQBsAFMATgA3AFIAYQA1AHUAZwArAEoAdgB0AGcAaABNADkAbgA0AGUAZwBNAGMARgB3AHcAOQArAEwAQQBiADQAbwB5AG8AMQA0AFUAVABKAGwAdwBJADIAWABzAEIAcwBTAFYAdQB0AGIAUABmAEUAMwBmAE8ARwBoAG4ARAB6ADcAMgBpAEIATgBHAFEAWABWADkAZwBkAGwAeQBXAGIALwA5AGUAUgB2AHMAUABKAC8ASABXADUAYQBKADEAbQBxAG4AbAA0ADkATgBEAFQARgBQAFcAegBVAEsAOAB6AGcAQgBFAFAAdgB3AG8AUwBaAGkAMAA1ADkATgBBAHcAOQA2AGoAZwBjAFMAdwBIAFoAMwBMAHoATABNADYAdQAvADgAcQA2AEwAeABEAEIAdwBCAHkAeABmAEkAWABvADYAZABzAEUAbABzAFQAYgA3ADUAdABsAGMAKwBCAFQAOABpAEIAawBkAHcAMwBMAGkAbQBOAEwASQBQAHgAaABYAEgAMwBBADIAQgA4AFgASgBXAFkATABOAHAAdgBjAE0AagB0AFoAKwBmAEgAZABMAHEASABiAHUATAB5AC8ATwBBAEMAZwBQAHgAUgBaAGwAdABRAHoAawByADEARgB4ADEATgBoAFMAOQBGAFYARQBOAGcAMwBpAGsAeQBNADUAUQBsAHQAaQBkAE8AMABRAD0APQA8AC8ATQBvAGQAdQBsAHUAcwA+AA0ACgAgACAAPABQAD4AdQBnAHUARwA0AC8AawBYAFIAYwAwADYAVAB2AHEAQQAwADcAVgBDACsAbwBTAHEAdgBwAGwANQBwAEEAYwBNAHoATwBCAGEAWABjAG4AUgBkAGsAcQBaAGQASgBJAGQAcwB6AFIAUgA4ADUAcwAxAEQANQBFAFcAZwBrAEsARgArAHMARAA5AG8ASABIADgAOQBaAFIAZQBGADcAMgBKAFEANQArAEQAQwA1AHYAcgAzAHcAeQBsAFUAWQBlAEUAdgBRAHAAZgBlAGwAYQBuAEsAWABNAHAAKwBiADYATQB5AGEAUABQADIAagB3ADkAeQBGAGMAOQA2AGEASgA1AC8ALwAwAHAANwBCAE8AdgB4AGEASgBBAFcAcgAyAEIALwBnAGIASwBOAFAAbgBNAGMAMAB2ADYALwAyAG4AYwBMAFAAYQBkADgAYwBUAEgASwBkAHMAPQA8AC8AUAA+AA0ACgAgACAAPABRAD4AMwBuAEMANgA2AGwAeQBtAFcAWQBUAFIAUgA1AE8AOQA3AFAAWQAvADQAMgB4AHUAVgBoAGEAagA2AEMASABJAFkARwBJADUAMABKACsASQBhAEMAWAA1AGkAMwBZAFYAagBDAFQAdwBsADcAOABIAEUAZAB0AE4AVAAvAGUARgAvAHYAVQBNAEYAVgBjAFgAZwBDAG8AQQBUAG4AbAB0AHkAUQAwAHkARwBsAC8AcQBrACsAQgBKAEMAcABVAHAAcAA2AFYAYgBGAFIARwBEAHcAcQBYAHYAZABzAGgASgBvAHcARAB5AFgAVAArAEcARQBRAFEAWQBhAGYAWgByAFYAcQB2AG0AZgBTAC8AbQBCAHkAeQA1AHQAVwArAHEAMABuAFYAWgBXADUAMgB0ADgAbABHAGgATwBTAHgAWQBJAEkAcgBtAHIARQBWADcAVgA4AE0APQA8AC8AUQA+AA0ACgAgACAAPABEAFAAPgBjAGEAeABYAFYALwBXADcAVgBZAGYAbQB4ADUAagBoAG0ALwBsAFYAbQBsAEgAegBMAFoAQQBDAHQAYwBrAGsAOQBnADEAaQB5AFkAbABsAGQAVAB4AHgAZABMAHQATwBjAEUAOQBZAGYAMQByAG0AMgBjAE0AUgBtAEwAZABpAHUAMgBxAHQAegBSAG0ANABzAEsAaABkAHMAVwAyADYAYgA5AG8ASAByAE4ARQB4AEYAbABEAFkAdABlADcAeQBtAEwAcQA4AHkAYgBnAG0AWABQAG8AagB2AEoAZgBEADEALwBSAEMATABKAHIAQQBPAFMAZQB0AEMAWgBLAGYAcgBsAHkAVgAvADgAdQByAGkAegByAHUAegBVAEsAYgBLAFkAWQBtAGkASwBYAFUAVwBPAEkAawAwADIAaAB3AEEAZgAwAFIAVgBrAGEAZwBWAGgAOAA9ADwALwBEAFAAPgANAAoAIAAgADwARABRAD4AcgBSAHAANwBTADEARABvAG4ANwBlAE4AaQBhADgAKwAyAFQANQBWAFAANgAyAHQATwA4ACsASQBwAEcAaABOADQARAB2AHcAbABCAHIAaQBjADMASgBCAC8AYgBiAGEAVQArAEsAWABYAE8ALwByAEYANABsAGIAWgBBAFQAVABRAFQATAAvAHcAVQBlAHkAWQBTAG4ATwBiAFUATgB3AEgAVQByAG8AbAA3AC8AVwA5AEYAZABSAFkAQwB2AHUAYQA2AGwAeAA5AFQAWgBaAEgARQB3ADMAeQAyADkAWQBaAHMASwBnADIAZABKAEsAaQBjAGwAdgBHAGwAOABLADcAegByADkATgBxAFQAawAxAE8AYQBJAGEAdAByAEkAeQA5AGwASQB2AGQAUQA2AGYAdAAzAHAAbwBtADIAUQA4ADEAcABFADQAdwBGADcAVwBEAE0APQA8AC8ARABRAD4ADQAKACAAIAA8AEkAbgB2AGUAcgBzAGUAUQA+AFMAYwBUAHIARgBjAEkAVABsAHYAVQBsAHkATABvAFEAawBnAHkANQA4AGkAawBGADYAbQBBADAAawBkAEkANwBVAFoATQBFAEcAbgAzAHoASwBGAHQAdQA3AFMAagBWAGEAZQByAFoAbAB4AHMAVwBKAGoASwBJAHEAVgA2AFAAOQA5AG8AMQBwADYANgAvADIAQwB0AFgAKwA2AEEAZQB3AEEAUwAzAEUARwBsAG0ARgBZAEwASwB4AEIANwBvAEIAVABKAHoALwBwADEAdwBvAHgAcAA4AEQAQQBhADQASQBJAFcAeQBZADEAWQBvAFgAZAAvAEMARQBvADEASgBZAFoAVwAxADIAagBLAFEAbAAzAFYAdgBsADEAWAB1ACsAdAA0AHcAcgBhAEoAZgAvAGsARgBUAGsAeQBJAHUAOABhADQAQQA2AHYAMwB0AHIAYQAwAD0APAAvAEkAbgB2AGUAcgBzAGUAUQA+AA0ACgAgACAAPABEAD4AVABGAGgASQB2AEcATABRAHQAVQBnAEEAUQAzAHEASwBPAGEAcQBLADgAZgBKAEQAcABTADgAUgBrAHIAOQBDAFAAcQArAHcATABPAEkAVgArAEMAZQByAFYAOQA0ADcAKwB2AGYAbQBjAGwASgBFAGUAUABHAC8AOQAwAEIASgBtAGIAdgBIADUAcQB0ADIAMABTAEwATwBGADQAaABsAFMAcwA1AEwAdwBvAEoAcQBGAEYAWgB3AFAAOQBPADIAVgBMAEUANQBSACsAMQBCAEsAegB6AEkAdgBwAEsAZABmAHMATgBiAFMAdQBkADIAUwBuAGUAYwBVAGYAUQBTAHQALwArAE4AMwA5AHoAWgAyAGwAdgBHAHIAVQBxAHEAeABNAG4AUAArAHoAagA3AEQAbwBzAGwAaABSAGsAKwBoAGgAawAxAGoARABEAGIAdwA3AEQAWQBIAE8AOABlAEMATAAxAFIAcAB2AGMATwBXAEgAUgBpAFYARQB1AFMAMABEAC8AbwAxAHIAaABpAGcAdQBqAHQAZgAwAGMAUgB6AEEAbABPAG8AdwBHAFIAUQBHAGsATQA2AFIARwBaADcAUwB1ADQATQB4AE8ANwBnAHIAYQBYADkAYwB0AGcAOQBuAFIAQwBMAHkAMwBoADcAVABvAGUAdwA1AEYAMQAxAEkARQArAHMAVwB2AHAALwBNAGQAbQA3AHQAdgBpAE0AMQB2ADEAbQBOAGwAKwB4AEIAbgBxADEARAA1AHIARgBtADMAVgBtAGUAcABIAHoATQAwAHMAVQBpAHkAZAArADMAQgA2AE8AQgAvADIANwBPAEMAVwBlAHgAWgBzADAAbgBwADAAbQBRAGUAUwByADQARQBKAHIATQBwAEcAMABiAGkASgA3AFEAPQA9ADwALwBEAD4ADQAKADwALwBSAFMAQQBQAGEAcgBhAG0AZQB0AGUAcgBzAD4A";

        public static string AESShareKey = "AHgAbQBsACAAdgBlAHIAcw";
    }
}