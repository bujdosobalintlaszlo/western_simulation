public class BiomChunk
{
    public Field[][] Fields { get; private set; }
    public int OriginRow { get; private set; }
    public int OriginCol { get; private set; }
    public string Type { get; private set; }

    public BiomChunk(Field[][] fields, int originRow, int originCol, string type)
    {
        Fields = fields;
        OriginRow = originRow;
        OriginCol = originCol;
        Type = type;
    }
}
