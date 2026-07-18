using DailyDuty.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyDuty.Interfaces;
using Dalamud.Plugin.Services;
using Lumina.Excel;

namespace DailyDuty.Classes;

/// <summary> For managing lumina data </summary>
public abstract class DataFile<T, TU> : IDataFile where T : DataFile<T, TU>, new() where TU : struct, IExcelRow<TU> {
    protected abstract string FileName { get; }

    public static async Task<T> Load() {
        var configFileName = new T().FileName;

        IPluginLog.Get().Debug($"Loading Data {configFileName}.datafile.json");
        return await Data.LoadCharacterData<T>($"{configFileName}.datafile.json");
    }

    public void Save() {
        IPluginLog.Get().Debug($"Saving Data {FileName}.datafile.json");
        Task.Run(() => Data.SaveCharacterData(this, $"{FileName}.datafile.json"));
    }

    public Dictionary<uint, LuminaDataEntry<TU>> LuminaData = [];

    public virtual void Update() { }
}
