/*
 * This file is part of Taichi.Demo project.
 *
 * (c) MuGuangyi <muguangyi@hotmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

using Taichi.Foundation;
using Taichi.Logger;
using Taichi.Asset;
using Taichi.Gameplay;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Taichi.Demo
{
    public class Player : GObjectActor
    {
        [Resolve] private static IEntityFactory factory = null;

        public string Name
        {
            get => Get<string>("Name");
            set => Set("Name", value);
        }

        protected override void OnInit()
        {
            base.OnInit();

            this.Info("Create player");

            AddTrait(typeof(Profile));
            AddFeature(typeof(ProfileFeature));

            this.Name = "New name";

            Retrieve().AssetReference = "Model/Cube";
            SetLocation(0, -2);
            SetRotation(45, 45, 45);
            SetScale(1, 2, 1.5f);

            var entityMgr = World.DefaultGameObjectInjectionWorld.EntityManager;
            factory.InstantiateAsync("Model/Sphere", entityMgr).Completed += a =>
            {
                var e = (Entity)a.GetResult();
                entityMgr.SetComponentData(e, new Translation { Value = new float3(0f, -0.5f, 0f) });
            };
        }

        protected override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);

            var angle = deltaTime * 60;
            SetRotation(this.Roll + angle, this.Pitch + angle, this.Yaw + angle);
        }
    }
}
