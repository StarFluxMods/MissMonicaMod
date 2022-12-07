using System.Reflection;
using KitchenData;
using KitchenLib;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;
using System.IO;
using Kitchen;
#if MELONLOADER
using MelonLoader;
#endif
#if BEPINEX
using BepInEx;
#endif

#if MELONLOADER
[assembly: MelonInfo(typeof(MissMonicaMod.Main), MissMonicaMod.Main.MOD_NAME, MissMonicaMod.Main.MOD_VERSION, MissMonicaMod.Main.MOD_AUTHOR)]
[assembly: MelonGame("It's Happening", "PlateUp")]
#endif
namespace MissMonicaMod
{
#if BEPINEX
    [BepInProcess("PlateUp.exe")]
    [BepInPlugin(MOD_ID, MOD_NAME, MOD_VERSION)]
#endif
	public class Main : BaseMod
	{
		public const string MOD_ID = "missmonicamod";
		public const string MOD_NAME = "Miss Monica Mod";
		public const string MOD_AUTHOR = "StarFluxGames";
		public const string MOD_VERSION = "0.1.0";
		public const string MOD_COMPATIBLE_VERSIONS = "1.1.2";

		public static AssetBundle bundle;

		public Main() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_COMPATIBLE_VERSIONS, Assembly.GetExecutingAssembly()) { }

		protected override void OnFrameUpdate()
		{
		}

		protected override void OnInitialise()
		{
#if WORKSHOP
			bundle = AssetBundle.LoadFromFile(Path.Combine(ResourceUtils.FindModPath(Assembly.GetExecutingAssembly(), AssetBundleLocation.ModsFolder), "missmonicamod"));
#endif
#if !WORKSHOP
			bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "missmonicamod"));
#endif

			AddGameDataObject<MissMonicaOutfit>();
		}
	}

	
	public class MissMonicaOutfit : CustomPlayerCosmetic
	{
		public override CosmeticType CosmeticType => CosmeticType.Outfit;
		public override GameObject Visual => MissMonicaMod.Main.bundle.LoadAsset<GameObject>("MissMonicaOutfit");

		public override void OnRegister(GameDataObject gameDataObject)
		{
			GameObject Prefab = ((PlayerCosmetic)gameDataObject).Visual;

			PlayerOutfitComponent playerOutfitComponent = Prefab.AddComponent<PlayerOutfitComponent>();
			
			playerOutfitComponent.Renderers.Add(GameObjectUtils.GetChildObject(Prefab, "Glasses").GetComponent<SkinnedMeshRenderer>());
			playerOutfitComponent.Renderers.Add(GameObjectUtils.GetChildObject(Prefab, "Golden Chef Hat").GetComponent<SkinnedMeshRenderer>());
			playerOutfitComponent.Renderers.Add(GameObjectUtils.GetChildObject(Prefab, "Miss Monica's Apron").GetComponent<SkinnedMeshRenderer>());
			playerOutfitComponent.Renderers.Add(GameObjectUtils.GetChildObject(Prefab, "Cylinder.001").GetComponent<SkinnedMeshRenderer>());

			playerOutfitComponent.Hats.Add(GameObjectUtils.GetChildObject(Prefab, "Golden Chef Hat").GetComponent<SkinnedMeshRenderer>());
			
			MaterialUtils.ApplyMaterial<SkinnedMeshRenderer>(Prefab, "Glasses", new Material[] { MaterialUtils.GetExistingMaterial("Piano Black") });
			MaterialUtils.ApplyMaterial<SkinnedMeshRenderer>(Prefab, "Golden Chef Hat", new Material[] { MaterialUtils.GetExistingMaterial("Paint - Gold") });
			MaterialUtils.ApplyMaterial<SkinnedMeshRenderer>(Prefab, "Miss Monica's Apron", new Material[] { MaterialUtils.GetExistingMaterial("Metal") });
			MaterialUtils.ApplyMaterial<SkinnedMeshRenderer>(Prefab, "Cylinder.001", new Material[] {
				MaterialUtils.GetExistingMaterial("Sea"),
				MaterialUtils.GetExistingMaterial("Sack - White"),
				MaterialUtils.GetExistingMaterial("Raw Fillet Skin 2"),
				MaterialUtils.GetExistingMaterial("Sea") });
		}
	}
	

}