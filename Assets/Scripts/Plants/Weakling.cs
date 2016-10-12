using UnityEngine;
using System.Collections;

public class Weakling : Plant {

    public override string getMaturity()
    {
        return "MATURE";
    }

    public override bool isPollinated()
    {
        return true;
    }

    public override int calculateScore()
    {
        return growth;
    }

}
