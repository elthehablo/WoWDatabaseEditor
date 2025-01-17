using System;
using System.Collections.ObjectModel;
using WDE.Common;
using WDE.Common.Database;

namespace WDE.LootEditor.Solution;

public class LootSolutionItem : ISolutionItem, IEquatable<LootSolutionItem>
{
    private readonly LootSourceType type;
    private readonly uint entry;
    private readonly uint difficulty; // todo: reconsider if the difficulty should be here

    public LootSolutionItem(LootSourceType type, uint entry, uint difficulty)
    {
        this.type = type;
        this.entry = entry;
        this.difficulty = difficulty;
    }

    public LootSourceType Type => type;
    
    public uint Entry => entry;

    public uint Difficulty => difficulty;

    public bool IsContainer => false;
    
    public ObservableCollection<ISolutionItem>? Items => null;
    
    public string? ExtraId => entry.ToString();
    
    public bool IsExportable => true;
    
    public ISolutionItem Clone()
    {
        return new LootSolutionItem(type, entry, difficulty);
    }
    
    public bool Equals(LootSolutionItem? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        // difficulty matters only for Creature loot type
        return type == other.type && entry == other.entry && (type != LootSourceType.Creature || difficulty == other.difficulty);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((LootSolutionItem)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)type, entry, type == LootSourceType.Creature ? difficulty : 0);
    }

    public static bool operator ==(LootSolutionItem? left, LootSolutionItem? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(LootSolutionItem? left, LootSolutionItem? right)
    {
        return !Equals(left, right);
    }

}