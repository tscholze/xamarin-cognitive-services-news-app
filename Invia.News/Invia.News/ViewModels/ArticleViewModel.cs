using Invia.News.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invia.News.ViewModels
{
    public class ArticleViewModel
    {
        #region Private member

        readonly Article articel;

        #endregion

        #region Public members

        public string UrlSource
        {
            get
            {
                return articel.Link;
            }
        }

        #endregion

        #region Init

        public ArticleViewModel(Article articel)
        {
            this.articel = articel;
        }

        #endregion
    }
}
