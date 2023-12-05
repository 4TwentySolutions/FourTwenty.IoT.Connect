using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces.Rules;

namespace FourTwenty.IoT.Server.Extensions
{
    public static class ActionsExtensions
    {
        public static IReadOnlyCollection<IAction> GetActions(this IReadOnlyCollection<IAction> actions, ActionType type)
        {
            return actions.Where(x => x.ActionType == type).ToList();
        }

        public static async Task ExecuteActions(this IReadOnlyCollection<IAction> actions, ActionType type, object value = null)
        {
            foreach (var action in actions.Where(x => x.ActionType == type))
            {
                await action.Execute(value);
            }
        }
    }
}
