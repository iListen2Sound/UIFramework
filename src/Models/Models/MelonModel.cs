using MelonLoader;
using System.Reflection;

namespace UIFramework.Models;

public class MelonModel : ModModelBase
{

    public override MelonBase Instance { get; set; }

    public MelonModel(MelonBase instance, List<MelonPreferences_Category> catList)
    {
        Instance = instance;
        try
        {
            Type type = instance.GetType();
            Assembly ass = type.Assembly;
            _displayName = ass.GetCustomAttribute<UIInfoAttribute>()?.DisplayName ?? Identifier;
        }
        catch (Exception ex) { Debug.Log($"{ex.Message}\n{ex}", false, 2); }

        foreach (MelonPreferences_Category cat in catList)
        {
            SubModels.Add(new MelonCategoryModel(cat, this));

        }
    }
    public MelonModel(MelonBase instance)
    {
        Instance = instance;
    }

}