namespace LaptopDecoder.Decoders;

public class ToHorizontalNonPairs1To18 : DecoderBase
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

            if (pair1Matching == false)
            {
                newValues[crossIndex] = new Value(values[crossIndex].ValueType, $"{crossIndex + 1}" + pair1);
                continue;
            }
            
            if (pair2Matching == false)
            {
                newValues[crossIndex] = new Value(values[crossIndex].ValueType, $"{crossIndex + 1}" + pair2);
                continue;
            }
            
            newValues[crossIndex] = new Value(values[crossIndex].ValueType, $"{crossIndex + 1}" + pair1[0] + pair2[0]);
        }

        return newValues;
    }

    public override DecoderResult DecodeValue(Value[] values, string key = "")
    {
        var values1 = values.Take(9).ToArray();
        var values2 = values.Skip(9).ToArray();
        var values1Result = ProcessValues(values1);
        var values2Result = ProcessValues(values2);
        var result = new ValueBase[]
        {
            values1Result[0], values1Result[1], values1Result[2], values2Result[0], values2Result[1], values2Result[2],
            values1Result[3], values1Result[4], values1Result[5], values2Result[3], values2Result[4], values2Result[5],
            values1Result[6], values1Result[7], values1Result[8], values2Result[6], values2Result[7], values2Result[8],
        };
        
        return new DecoderResult(result);
    }

    Value[] ProcessValues(Value[] values)
    {
        var newValues = new Value[values.Length];
        for (var i = 0; i < values.Length; i++)
        {
            newValues[i] = new Value(values[i].ValueType, $"{i}{values[i].TheValue}");
        }

        return newValues;
    }
}