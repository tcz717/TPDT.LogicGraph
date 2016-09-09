using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class InstantCard:Card
{
    public override bool CanUse()
    {
        return Owner.Activited;
    }
}
