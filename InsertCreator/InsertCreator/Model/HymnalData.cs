﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Liedeinblendung.Model
{
    class HymnalData
    {

        public string Book { get; set; } = "";

        public string Number { get; set; } = "";

        public string SongVerses { get; set; } = "";

        public string Name { get; set; } = "";

        public string TextAutor { get; set; } = "";

        public string MelodieAutor { get; set; } = "";

       
        public HymnalData(string book, string number, string name, string textAutor, string melodieAutor, ObservableCollection<SelectedVerse> selectedVerses)
        {
            Book = book;
            Number = number;
            Name = name;
            TextAutor = textAutor;
            MelodieAutor = melodieAutor;
            SongVerses = WriteInputVers(selectedVerses);
        }

        private string WriteInputVers(ObservableCollection<SelectedVerse> verses)
        {


            string state = "";

            string inputVerse = "";

            foreach (var verse in verses)
            {
                if (verse.IsSelected)
                    state += "T";
                else
                    state += "F";
            }

            List<Match> matches = new List<Match>();

            foreach (Match match in Regex.Matches(state, "[T]{1,}"))
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
    }
}
