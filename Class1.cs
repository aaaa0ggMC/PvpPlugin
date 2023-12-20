using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace TestPlugin
{
    [ApiVersion(2, 1)]//api版本
    public class TestPlugin : TerrariaPlugin
    {
        /// 插件作者
        public override string Author => "aaaa0ggmc";

        /// 插件说明
        public override string Description => "Pvp Plugin";

        /// 插件名字
        public override string Name => "Pvp Helper";

        /// 插件版本
        public override Version Version => new Version(1, 0, 0, 0);

        public TestPlugin(Main game) : base(game)
        {
        }






        /// <summary>
        /// Initializes a new instance of the TestPlugin class.
        /// This is where you set the plugin's order and perfrom other constructor logic
        /// </summary>
        /// 插件处理

        /// <summary>
        /// Handles plugin initialization. 
        /// Fired when the server is started and the plugin is being loaded.
        /// You may register hooks, perform loading procedures etc here.
        /// </summary>
        /// 插件启动时，用于初始化各种狗子
        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);//钩住游戏初始化时
        }





        private void OnInitialize(EventArgs args)//游戏初始化的狗子
        {
            //Adding a command is as simple as adding a new ``Command`` object to the ``ChatCommands`` list.
            //The ``Commands` object is available after including TShock in the file (`using TShockAPI;`)
            //第一个是权限，第二个是子程序，第三个是名字

            Command a = new Command("clearPlayerInventory", clearInventPl, "cpi")
            {
                HelpText = "输入 /cpi 或者 clearInventPl 会清除某个玩家背包"
            };
            a.Permissions.Add(Permissions.su);

            Commands.ChatCommands.Add(a);


        }




        private void clearInventPl(CommandArgs args)
        {
            if(args.Parameters.Count < 2)
            {
                Console.Error.WriteLine("用法 /cpi [PlayerName]");
                return;
            }
            string pln = args.Parameters[1];
            TShockAPI.DB.UserAccount user = TShock.UserAccounts.GetUserAccountByName(pln);
            if (user == null)
            {
                args.Player.SendErrorMessage("Player " + pln + " can't be found.");
                return;
            }
            TSPlayer player;
            var found = TShockAPI.TSPlayer.FindByNameOrID(pln);
            if (found.Count == 1)
            {
                player = (TSPlayer)found[0];
                player.TPlayer.inventory = new Item[0];
            }
            else
            {
                return;
            }
        }
    }
}
