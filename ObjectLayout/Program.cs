using ObjectLayout;
using ObjectLayoutInspector;

TypeLayout.PrintLayout<SampleStruct>();
Console.Read();

[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
public struct SampleStruct
{
    public byte X;
    public double Y;
    public int Z;
}

