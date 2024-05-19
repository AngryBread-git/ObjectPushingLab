using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FormattedString
{
    private string _content;
    private ContentType _contentType;
    public EventInfo _d;

    public string Content
    {
        set { _content = value; }
        get { return _content; }
    }

    public ContentType ContentType
    {
        set { _contentType = value; }
        get { return _contentType; }
    }

    public FormattedString(string givenContent, ContentType givenContentType)
    {
        _content = givenContent;
        _contentType = givenContentType;
        _d = new DecreaseTypingDelayEventInfo();
    }

}

