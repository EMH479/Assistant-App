using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace AI_Assistant.Services
{
    public class VoiceService
    {
        private readonly SpeechConfig _speechConfig;
        public VoiceService(string subscriptionKey, string region)
        {
            _speechConfig = SpeechConfig.FromSubscription(subscriptionKey, region);
        }

        public async Task<string> RecognizeSpeechAsync()
        {
            using var recognizer = new SpeechRecognizer(_speechConfig);
            var result = await recognizer.RecognizeOnceAsync();
            return result.Text;
        }

        public async Task SynthesizeSpeechAsync(string text)
        {
            using var synthesizer = new SpeechSynthesizer(_speechConfig);
            await synthesizer.SpeakTextAsync(text);
        }
    }
}
