using System;

namespace DataOperation.Model
{
public class GradeField
{
    public DateTime CompletedDate { get; set; }
    public string Paragraph { get; set; }
    public double Speed { get; set; }
    public int BackSpace { get; set; }
    public double HitKey { get; set; }
    public double KeyLong { get; set; }
    public int WrongWords { get; set; }
    public int WordsCount { get; set; }
    public int KeyCount { get; set; }
    public string CostTime { get; set; }
}
}
