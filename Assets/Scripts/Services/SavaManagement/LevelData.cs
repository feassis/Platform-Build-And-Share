using System.Collections.Generic;

[System.Serializable] 
public class LevelData 
{
    public string Name;
    public List<TileRecord> backgroundtiles = new List<TileRecord>(); 
    public List<TileRecord> beforegroundtiles = new List<TileRecord>(); 
    public List<TileRecord> groundtiles = new List<TileRecord>(); 
    public List<TileRecord> interactablesgroundtiles = new List<TileRecord>(); 
    public List<TileRecord> decorationgroundtiles = new List<TileRecord>(); 
    public List<TileRecord> foregroundtiles = new List<TileRecord>(); 

    public List<InteractablesRocord> interactables = new List<InteractablesRocord>();
}
