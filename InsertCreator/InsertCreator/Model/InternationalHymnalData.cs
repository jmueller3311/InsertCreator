using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HgSoftware.InsertCreator.Model
{
    public class InternationalHymnalData : IInsertData
    {
        #region Public Constructors
        public InternationalHymnalData(string book, string number, string en, string wa, string ua, string ru, ObservableCollection<SelectedVerse> selectedVerses)
        {
            Book = book;
            Number = number;
            EN = en;
            WA = wa;
            UA = ua;
            RU = ru;
            SongVerses = WriteInputVers(selectedVerses);
        }
        #endregion Public Constructors

        #region Public Properties
        public string Book { get; set; }
        public string Number { get; set; }

        public string FirstLine => $"EN: {EN} WA: {WA} UA: {UA} RU: {RU}";
        public string EN { get; set; }
        public string WA { get; set; }
        public string UA { get; set; }
        public string RU { get; set; }
        public string SongVerses { get; set; }
        public string SecondLine => $"{Book} {Number}";

        #endregion Public Properties


        #region Private Methods

        private string WriteInputVers(ObservableCollection<SelectedVerse> verses)
        {
            StringBuilder state = new StringBuilder("");

            string inputVerse = "";

            foreach (var verse in verses)
            {
                if (verse.IsSelected)
                    state.Append("T");
                else
                    state.Append("F");
            }

            List<Match> matches = new List<Match>();

            foreach (Match match in Regex.Matches(state.ToString(), "[T]{1,}"))
            {
                matches.Add(match);
            }

            if (matches.Count > 0)
            {
                inputVerse = " / ";

                foreach (var match in matches)
                {
                    switch (match.Length)
                    {
                        case 1:
                            inputVerse = $"{inputVerse}{match.Index + 1}";
                            break;

                        case 2:
                            inputVerse = $"{inputVerse}{match.Index + 1}-{match.Index + match.Length}";
                            break;

                        default:
                            inputVerse = $"{inputVerse}{match.Index + 1}-{match.Index + match.Length}";
                            break;
                    }

                    inputVerse = $"{inputVerse} + ";
                }

                char[] charsToTrim = { ' ', '+' };
                inputVerse = inputVerse.TrimEnd(charsToTrim);
                return inputVerse;
            }
            return inputVerse;
        }

        #endregion Private Methods
    }
}
