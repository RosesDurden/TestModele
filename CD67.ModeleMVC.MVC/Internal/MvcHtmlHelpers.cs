using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace CD67.ModeleMVC.MVC
{
    public static class MvcHtmlHelpers
    {
        /// <summary>
        /// Pour utiliser ce Helper :
        /// Ajouter un using en haut de la page cshtml : @using CD67.Jarvis.MVC.Internal
        /// Appeler comme les autres méthodes le nouveau helper : @Html.DescriptionFor(model => model.CHAMPBDD)
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);
            var description = metadata.Description;

            return MvcHtmlString.Create(string.Format(@"<span>{0}</span>", description));
        }

        /// <summary>
        /// Affiche le "display name" pour un élément d'énumération
        /// </summary>
        /// <param name="item">Elément d'énumaration courant</param>
        /// <param name="value">Valeur souhaitée</param>
        /// <returns>Valeur de l'annotation Display name de l'élément d'énumération courant</returns>
        public static HtmlString EnumDisplayNameFor(this Enum item, DisplayValue value)
        {
            var type = item.GetType();
            var member = type.GetMember(item.ToString());
            DisplayAttribute displayAttribute = (DisplayAttribute)member[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

            if (displayAttribute != null)
            {
                switch (value)
                {
                    case DisplayValue.Name:
                        return new HtmlString(displayAttribute.Name);
                    case DisplayValue.Prompt:
                        return new HtmlString(displayAttribute.Prompt);
                    case DisplayValue.Description:
                        return new HtmlString(displayAttribute.Description);
                    default:
                        return new HtmlString(item.ToString());
                }
            }

            return new HtmlString(item.ToString());
        }

        public enum DisplayValue
        {
            Name,
            Prompt,
            Description
        }
    }
}
