namespace LaptopDecoder.Decoders;

public class ToHorizontalPairs1To18 : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, string key = "")
    {
        var result = ProcessCrossess(values).Select(v => (ValueBase)v).ToArray();
        return new DecoderResult(result);
    }

    Value[] ProcessCrossess(Cross[] values)
    {
        var newValues = new Value[values.Length];
        for (var crossIndex = 0; crossIndex < values.Length; crossIndex++)
        {
            var pair1 = values[crossIndex].Values[0] + values[crossIndex].Values[1];
            var pair2 = values[crossIndex].Values[2] + values[crossIndex].Values[3];
            var pair1Matching = pair1.Distinct().Count() == 1; 
            var pair2Matching = pair2.Distinct().Count() == 1;

            if (pair1Matching)
            {
                newValues[crossIndex] = new Value(values[crossIndex].ValueType, $"{crossIndex + 1}" + pair1);
                continue;
            }
            
            if (pair2Matching)
            {
                newValues[crossIndex] = new Value(values[crossIndex].ValueType, $"{crossIndex + 1}" + pair2);
                continue;
            }
            
            newValues[crossIndex] = new Value(values[crossIndex].ValueType, $"{crossIndex + 1}" + pair1 + pair2);
        }

        return newValues;
    }

    public override DecoderResult DecodeValue(Value[] values, string key = "")
    {
        var result = new List<ValueBase>();
        result.AddRange(values);
        return new DecoderResult(result.ToArray());
    }
}