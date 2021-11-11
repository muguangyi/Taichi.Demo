using Taichi.Binding;
using Taichi.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Taichi.Demo
{
    public class LoginWindow : Window<LoginWindowModel>
    {
        protected override void OnOpen()
        {
            base.OnOpen();

            var input = this.GameObject.transform.Find("InputField").GetComponent<InputField>();
            Bind(new UnityNode(input, "onValueChange"), new ViewModelNode(this.ViewModel, "UserName"));

            var txtComponent = this.GameObject.transform.Find("UserName").GetComponent<Text>();
            Bind(new ViewModelNode(this.ViewModel, "UserName"), new UnityNode(txtComponent, "text"));

            var btn = this.GameObject.transform.Find("Button").GetComponent<Button>();
            Bind(new UnityNode(btn, "onClick"), new ViewModelNode(this.ViewModel, "Command"));

            var message = this.GameObject.transform.Find("Message").GetComponent<Text>();
            Bind(new UnityNode(input, "onValueChange"), new UnityNode(message, "text"));
        }
    }

    public class LoginWindowModel : ViewModel
    {
        private string userName = string.Empty;

        public LoginWindowModel()
        {
            this.Command = new ActionCommand(() =>
            {
                this.UserName = $"name{Random.Range(1, 10)}";
            });
        }

        public string UserName
        {
            get => this.userName;
            set => Set(ref this.userName, value, nameof(UserName));
        }

        public ICommand Command { get; } = null;
    }
}
