using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GreatQuotes
{
    public class QuoteManager
    {

       
        public static QuoteManager Instance { get; private set; }

        readonly IQuoteLoader loader;
        readonly ITextToSpeech textToSpeech;
        public IList<GreatQuote> Quotes { get; private set; }

        //private QuoteManager()
        //{
        //    loader = QuoteLoaderFactory.Create();
        //    Quotes = new ObservableCollection<GreatQuote>(loader.Load());
        //}
        public QuoteManager(IQuoteLoader loader, ITextToSpeech tts)
        {
            if (Instance != null)
            {
                throw new Exception("Can only create a single QuoteManager.");
            }
            Instance = this;
            this.loader = loader;
            this.textToSpeech = tts;
            Quotes = new ObservableCollection<GreatQuote>(loader.Load());
        }
        public void SayQuote(GreatQuote quote)
        {
            if (quote == null)
                throw new ArgumentNullException(nameof(quote));

            //ITextToSpeech tts = ServiceLocator.Instance.Resolve<ITextToSpeech>();
            ITextToSpeech tts = this.textToSpeech;

            var text = quote.QuoteText;

            if (!string.IsNullOrWhiteSpace(quote.Author))
                text += $" by {quote.Author}";

            tts.Speak(text);
        }

        public void Save()
        {
            loader.Save(Quotes);
        }
    }
}