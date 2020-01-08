namespace SCA.Shared.CustomController
{
    public abstract class ScaController : Microsoft.AspNetCore.Mvc.Controller
    {
        public abstract void SetToken(string token);

        protected abstract void Prepare();
    }
}
