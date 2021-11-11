/*
 * This file is part of Taichi.Demo project.
 *
 * (c) MuGuangyi <muguangyi@hotmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

using Taichi.Binding;
using Taichi.Gameplay;
using Taichi.Logger;

namespace Taichi.Demo
{
    [RequireValue("Name")]
    public class ProfileFeature : Feature
    {
        public string Name
        {
            get => this.Actor.Get<string>("Name");
            set => this.Actor.Set("Name", value);
        }

        [OnChangeValue("Name")]
        public void OnNameChanged(object sender, ValueChangedArgs args)
        {
            this.Info($"Change name to {args.Value}");
        }
    }
}
