using UnityEngine;

public class GraphicsSource : MonoBehaviour
{
	public Material[][] materials;

	public float height;

	public float width;

	public int dif_materials { get; set; }

	private void Start()
	{
		dif_materials = 35;
		materials = new Material[2][];
		materials[0] = new Material[dif_materials];
		materials[1] = new Material[dif_materials];
		materials[0][0] = (Material)Resources.Load("Materials/mainMenu");
		materials[0][1] = (Material)Resources.Load("Materials/mainMenu2");
		materials[0][2] = (Material)Resources.Load("Materials/packLock");
		materials[0][3] = (Material)Resources.Load("Materials/packUnlock");
		if (GraphicsHD())
		{
			materials[0][4] = (Material)Resources.Load("Materials/modal");
			materials[0][5] = (Material)Resources.Load("Materials/staff");
			materials[0][6] = (Material)Resources.Load("Materials/magnets");
			materials[0][7] = (Material)Resources.Load("Materials/BtnInGame");
			materials[0][8] = (Material)Resources.Load("Materials/pack_1_back");
			materials[0][9] = (Material)Resources.Load("Materials/packBack2");
			materials[0][10] = (Material)Resources.Load("Materials/box_atlas_2");
			materials[0][11] = (Material)Resources.Load("Materials/box_atlas");
			materials[0][12] = (Material)Resources.Load("Materials/GearInGame");
			materials[0][13] = (Material)Resources.Load("Materials/packBack3");
			materials[0][14] = (Material)Resources.Load("Materials/packBack4");
			materials[0][15] = (Material)Resources.Load("Materials/comics2Back");
			materials[0][16] = (Material)Resources.Load("Materials/comics3Back");
			materials[0][17] = (Material)Resources.Load("Materials/comics4Back");
			materials[0][18] = (Material)Resources.Load("Materials/comics2Staff");
			materials[0][19] = (Material)Resources.Load("Materials/comics3Stuff");
			materials[0][20] = (Material)Resources.Load("Materials/comics4Stuff");
			materials[0][21] = (Material)Resources.Load("Materials/splesh_congratulations");
			materials[0][22] = (Material)Resources.Load("Materials/bubbles");
			materials[0][23] = (Material)Resources.Load("Materials/boxBubble");
			materials[0][24] = (Material)Resources.Load("Materials/robo3_boxBubble");
			materials[0][25] = (Material)Resources.Load("Materials/robo1_p3_startBubble");
			materials[0][26] = (Material)Resources.Load("Materials/robo2_p3_startBubble");
			materials[0][27] = (Material)Resources.Load("Materials/robo4_p4_startBubble");
			materials[0][28] = (Material)Resources.Load("Materials/preloader");
			materials[0][29] = (Material)Resources.Load("Materials/alert");
			materials[0][30] = (Material)Resources.Load("Materials/comicsIntro_building");
			materials[0][31] = (Material)Resources.Load("Materials/factoryInSide");
			materials[0][32] = (Material)Resources.Load("Materials/boxes");
			materials[0][33] = (Material)Resources.Load("Materials/tapToContinue");
			materials[0][34] = (Material)Resources.Load("Materials/device");
			materials[1][0] = (Material)Resources.Load("Materials/mainMenu_HD");
			materials[1][1] = (Material)Resources.Load("Materials/mainMenu2_HD");
			materials[1][2] = (Material)Resources.Load("Materials/packLock_HD");
			materials[1][3] = (Material)Resources.Load("Materials/packUnlock_HD");
			materials[1][4] = (Material)Resources.Load("Materials/modal_HD");
			materials[1][5] = (Material)Resources.Load("Materials/staff_HD");
			materials[1][6] = (Material)Resources.Load("Materials/magnets_HD");
			materials[1][7] = (Material)Resources.Load("Materials/BtnInGame_HD");
			materials[1][8] = (Material)Resources.Load("Materials/pack_1_back_HD");
			materials[1][9] = (Material)Resources.Load("Materials/packBack2_HD");
			materials[1][10] = (Material)Resources.Load("Materials/box_atlas_2_HD");
			materials[1][11] = (Material)Resources.Load("Materials/box_atlas_HD");
			materials[1][12] = (Material)Resources.Load("Materials/GearInGame_HD");
			materials[1][13] = (Material)Resources.Load("Materials/packBack3_HD");
			materials[1][14] = (Material)Resources.Load("Materials/packBack4_HD");
			materials[1][15] = (Material)Resources.Load("Materials/comics2Back_HD");
			materials[1][16] = (Material)Resources.Load("Materials/comics3Back_HD");
			materials[1][17] = (Material)Resources.Load("Materials/comics4Back_HD");
			materials[1][18] = (Material)Resources.Load("Materials/comics2Staff_HD");
			materials[1][19] = (Material)Resources.Load("Materials/comics3Stuff_HD");
			materials[1][20] = (Material)Resources.Load("Materials/comics4Stuff_HD");
			materials[1][21] = (Material)Resources.Load("Materials/splesh_congratulations_HD");
			materials[1][22] = (Material)Resources.Load("Materials/bubbles_HD");
			materials[1][23] = (Material)Resources.Load("Materials/boxBubble_HD");
			materials[1][24] = (Material)Resources.Load("Materials/robo3_boxBubble_HD");
			materials[1][25] = (Material)Resources.Load("Materials/robo1_p3_startBubble_HD");
			materials[1][26] = (Material)Resources.Load("Materials/robo2_p3_startBubble_HD");
			materials[1][27] = (Material)Resources.Load("Materials/robo4_p4_startBubble_HD");
			materials[1][28] = (Material)Resources.Load("Materials/preloader_HD");
			materials[1][29] = (Material)Resources.Load("Materials/alert_HD");
			materials[1][30] = (Material)Resources.Load("Materials/comicsIntro_building_HD");
			materials[1][31] = (Material)Resources.Load("Materials/factoryInSide_HD");
			materials[1][32] = (Material)Resources.Load("Materials/boxes_HD");
			materials[1][33] = (Material)Resources.Load("Materials/tapToContinue_HD");
			materials[1][34] = (Material)Resources.Load("Materials/device_HD");
			StartSeek("Menu");
			StartSeek("level_complited_window");
			StartSeek("level_failed_window");
			StartSeek("level_paused_window");
			StartSeek("preloader_menu");
			StartSeek("pack_help_window");
			StartSeek("Scene");
		}
	}

	public bool GraphicsHD()
	{
		float num = Screen.width;
		float num2 = Screen.height;
		if ((num >= width && num2 >= height) || (num2 >= width && num >= height))
		{
			return true;
		}
		return false;
	}

	private void StartSeek(string name)
	{
		if ((bool)GameObject.Find(name) && (bool)GameObject.Find(name).GetComponent<GraphicsManager>())
		{
			GameObject.Find(name).GetComponent<GraphicsManager>().Go();
		}
	}
}
