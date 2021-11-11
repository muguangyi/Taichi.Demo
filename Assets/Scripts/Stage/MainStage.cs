/*
 * This file is part of Taichi.Demo project.
 *
 * (c) MuGuangyi <muguangyi@hotmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

using Taichi.Gameplay;

namespace Taichi.Demo
{
    public class MainStage : Stage
    {
        private IFrontend ui = null;
        private Player player = null;

        protected override void OnInit()
        {
            base.OnInit();

            this.ui = new Overlayer();
            this.ui.OpenWindow("Login", typeof(LoginWindow));

            this.player = new Player();
        }
    }
}
