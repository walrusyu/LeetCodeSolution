
using System.Collections.Generic;

public class Node
{
    public Node(char value)
    {
        Value = value;
        Children = new List<Node>();
    }
    public char Value { get; set; }
    public List<Node> Children { get; set; }
}