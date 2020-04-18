using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUpgrader
{
    private delegate State UpgradeFunc(State state);

    private static List<UpgradeFunc> upgradeFuncs = new List<UpgradeFunc>()
    {
        UpgradeTo0,
        UpgradeTo1,
        UpgradeTo2
    };
    public static int Version { get; } = 2;


    public static bool CanUpgrade(State state)
    {
        return state.version != Version;
    }

    public static State Upgrade (State state)
    {
        while (state.version < Version) state = upgradeFuncs[state.version + 1](state);
        return state;
    }

    private static State UpgradeTo0(State state)
    {
        state.version = 0;
        return state;
    }

    private static State UpgradeTo1(State state)
    {
        state.version = 1;
        return state;
    }

    private static State UpgradeTo2(State state)
    {
        state.clockEnabled = true;
        state.version = 2;
        return state;
    }



}

public class MapUpgrader
{
    private delegate Map UpgradeFunc(Map map);

    private static List<UpgradeFunc> upgradeFuncs = new List<UpgradeFunc>()
    {
        UpgradeTo0,
        UpgradeTo1
    };
    public static int Version { get; } = 1;

    public static bool CanUpgrade(Map map)
    {
        return map.version != Version;
    }

    public static Map Upgrade(Map map)
    {
        while (map.version < Version) map = upgradeFuncs[map.version + 1](map);
        return map;
    }

    private static Map UpgradeTo0(Map map)
    {
        map.version = 0;
        return map;
    }

    private static Map UpgradeTo1(Map map)
    {
        map.version = 1;
        return map;
    }
}
