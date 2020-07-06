namespace MicroservicesPOC.Shared.Common.Models
{
    public abstract class QuestionAnswerDTO
    {
        public string QuestionCode { get; set; }

        public abstract QuestionType Type { get; }

        public abstract object GetAnswer();
    }

    public abstract class QuestionAnswerDTO<T> : QuestionAnswerDTO
    {
        public T Answer { get; set; }

        public override object GetAnswer() => this.Answer;
    }

    public class NumericQuestionAnswerDTO : QuestionAnswerDTO<decimal>
    {
        public override QuestionType Type => QuestionType.Numeric;
    }

    public class TextQuestionAnswerDTO : QuestionAnswerDTO<string>
    {
        public override QuestionType Type => QuestionType.Text;
    }

    public class ChoiceQuestionAnswerDTO : QuestionAnswerDTO<string>
    {
        public override QuestionType Type => QuestionType.Choice;
    }
}
