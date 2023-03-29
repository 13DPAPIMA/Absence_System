using System;
using System.IO;

class Lessons : AbsenceSystem
{
    public Lessons(string Path) : base(Path) { }

    public override void WriteText()
    {
        base.WriteText();
    } 

    public override void ReadFile()
    {
        base.ReadFile();
    }

    public override void DeleteText()
    {
        base.DeleteText();
    }

    public override void Filter()
    {
        base.Filter();
    }
}
