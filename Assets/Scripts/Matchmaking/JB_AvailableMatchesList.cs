using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking.Match;
//using System.Threading.Tasks;

public static class JB_AvailableMatchesList
{
    public static event Action<List<MatchInfoSnapshot>> OnAvailableMatchesChanged = delegate { };

    private static List<MatchInfoSnapshot> matches = new List<MatchInfoSnapshot>();

    public static void HandleNewMatchList(List<MatchInfoSnapshot> matchList)
    {
        matches = matchList;
        OnAvailableMatchesChanged(matches);
    }
}

