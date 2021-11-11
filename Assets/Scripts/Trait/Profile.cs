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

namespace Taichi.Demo
{
    public class Profile : Trait
    {
        private string name = string.Empty;

        [Accessible]
        public string Name
        {
            get => this.name;
            set
            {
                this.name = value;
                NotifyValueChanged(this, new ValueChangedArgs("Name", value));
            }
        }
    }
}
