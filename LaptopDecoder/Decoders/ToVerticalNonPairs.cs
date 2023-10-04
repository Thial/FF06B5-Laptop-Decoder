namespace LaptopDecoder.Decoders;

public class ToVerticalNonPairs : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var newValues = new ValueBase[values.Length];
        var enc = values[0].Encoding;
        for (var crossIndex = 0; crossIndex < values.Length; crossIndex++)
        {
            var pair1 = values[crossIndex].Values[0].TheValue + values[crossIndex].Values[2].TheValue;
            var pair2 = values[crossIndex].Values[1].TheValue + values[crossIndex].Values[3].TheValue;
            var pair1Matching = pair1.Distinct().Count() == 1; 
            var pair2Matching = pair2.Distinct().Count() == 1;

            if (pair1Matching == false)
            {
                newValues[crossIndex] = new Value(values[crossIndex].ValueType, pair1, enc);
                continue;
            }
            
            if (pair2Matching == false)
            {
                newValues[crossIndex] = new Value(values[crossIndex].ValueType, pair2, enc);
                continue;
            }
            
            newValues[crossIndex] = new Value(values[crossIndex].ValueType, string.Empty, enc);
        }

        return new DecoderResult(newValues);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var newValues = new ValueBase[values.Length];
        var enc = values[0].Encoding;
        for (var i = 0; i < values.Length; i++)
        {
            newValues[i] = new Value(values[i].ValueType, values[i].TheValue, enc);
        }
        
        return new DecoderResult(newValues);
    }
}