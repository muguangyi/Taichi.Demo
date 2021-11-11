/*
 * This file is part of Taichi.Demo project.
 *
 * (c) MuGuangyi <muguangyi@hotmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

using ILRuntime.Runtime.Intepreter;
using Taichi.ILRuntime;

namespace Taichi.Demo
{
    [ScriptAdaptor]
    public sealed class CrossClassAdaptor : CrossClass, IScriptAdaptor
    {
        private readonly IScriptContext context = null;
        private readonly IScriptMethod methodOptionalMethod = null;

        public CrossClassAdaptor(IScriptContext context)
        {
            this.context = context;
            this.methodOptionalMethod = context.GetMethod("OptionalMethod", 1);
        }

        public ILTypeInstance ILInstance => this.context.Instance;

        public override void OptionalMethod(params object[] args)
        {
            this.methodOptionalMethod?.Invoke(new object[] { args });
        }
    }
}
