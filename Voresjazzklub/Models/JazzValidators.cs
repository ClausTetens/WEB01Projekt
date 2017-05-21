using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Voresjazzklub.Models;

//namespace Voresjazzklub.Models {
namespace System.Web.Mvc.Html { 
    public static class JazzValidators {

            public static MvcHtmlString GenericValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes) {
                TagBuilder containerDivBuilder = new TagBuilder("span");
                containerDivBuilder.AddCssClass("field-validation-valid text-danger");
                //midDivBuilder.InnerHtml = helper.ValidationMessageFor(expression).ToString();
                containerDivBuilder.InnerHtml = "Her er en fejl";
                return MvcHtmlString.Create(containerDivBuilder.ToString(TagRenderMode.Normal));
            }

        public static MvcHtmlString JazzValidationMessageForCreateUserId<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes) {
            
            TagBuilder containerDivBuilder = new TagBuilder("span");
            containerDivBuilder.AddCssClass("field-validation-valid text-danger");
            //midDivBuilder.InnerHtml = helper.ValidationMessageFor(expression).ToString();
            //if(((UsersTableModel)helper).userExists()) {
            //containerDivBuilder.InnerHtml = ((UsersTableModel)(helper.ViewData.Model)).brugerId;
            containerDivBuilder.InnerHtml = helper.ValidationMessageFor(expression).ToString();
           // }
            return MvcHtmlString.Create(containerDivBuilder.ToString(TagRenderMode.Normal));
        }
    }
}