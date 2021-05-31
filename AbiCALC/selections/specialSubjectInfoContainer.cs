using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC.selections
{
    public class specialSubjectInfoContainer
    {
        public sepecialSubjectInfoDefinedOrder data1 = null;
        public specialSubjectInfoUndefinedOrder data2 = null;
        public List<abiexam> abis;

        public bool isDefined
        {
            get
            {
                if (data1 != null ^ data2 != null)
                {
                    return data1 != null;
                }
                else
                {
                    throw new Exception("data1 and data2 can not be set both or neither.");
                }
            }
        }

        public void update(List<abiexam> a)
        {
            abis = a;
            if (!isDefined)
            {
                bool b = false;
                subjectTypes s = null, s2 = null;
                foreach (abiexam abi in a)
                {
                    if (abi.type == data2.notDefined1 || abi.type == data2.notDefined2)
                    {
                        b = true;
                        s = abi.type == data2.notDefined1 ? data2.notDefined1 : data2.notDefined2;
                        s2 = abi.type != data2.notDefined1 ? data2.notDefined1 : data2.notDefined2;
                        break;
                    }
                }
                if (b)
                {
                    data1 = new sepecialSubjectInfoDefinedOrder();
                    data1.continueExtra12 = true;
                    data1.extra = s2;
                    if (data2.defined.t == subjectTypes.type.Naturwissenschaft)
                    {
                        data1.science1 = data2.defined;
                        data1.language1 = s;
                    }
                    else
                    {
                        data1.language1 = data2.defined;
                        data1.science1 = s;
                    }
                    data2 = null;
                }
            }
        }

        public specialSubjectInfoContainer(selection parent)
        {
            subjectTypes.type t = parent.getExtras()[parent.Index2];
            if (t == subjectTypes.type.Informatik)
            {
                data1 = new sepecialSubjectInfoDefinedOrder();
                data1.science1 = new subjectTypes(subjectTypes.type.Naturwissenschaft, ((selection.ScienceType)(parent.NaturWissenschaftsIndex)).ToString());
                data1.language1 = new subjectTypes(subjectTypes.type.Fremdsprache, parent.FremdsprachenIndex);
                data1.extra = new subjectTypes(subjectTypes.type.Informatik);
                data1.continueExtra12 = (bool)parent.Extra12;
            }
            else
            {
                if (parent.ps.forceExtraLanguage)
                {
                    data1 = new sepecialSubjectInfoDefinedOrder();
                    data1.science1 = new subjectTypes(subjectTypes.type.Naturwissenschaft, ((selection.ScienceType)(parent.NaturWissenschaftsIndex)).ToString());
                    data1.language1 = new subjectTypes(subjectTypes.type.Fremdsprache, parent.FremdsprachenIndex);
                    data1.extra = new subjectTypes(subjectTypes.type.Fremdsprache, new List<string>(parent.getExtras().Keys)[0]);
                    data1.continueExtra12 = (bool)parent.Extra12;
                }
                else
                {
                    if (!(bool)parent.Extra12)
                    {
                        data1 = new sepecialSubjectInfoDefinedOrder();
                        data1.language1 = new subjectTypes(subjectTypes.type.Naturwissenschaft, ((selection.ScienceType)(parent.NaturWissenschaftsIndex)).ToString());
                        data1.language1 = new subjectTypes(subjectTypes.type.Fremdsprache, parent.FremdsprachenIndex);
                        data1.extra = new subjectTypes(parent.getExtras()[parent.Index2], parent.Index2);
                        data1.continueExtra12 = (bool)parent.Extra12;
                    }
                    else
                    {
                        if (parent.getExtras()[parent.Index2] == subjectTypes.type.Fremdsprache)
                        {
                            data2 = new specialSubjectInfoUndefinedOrder();
                            data2.defined = new subjectTypes(subjectTypes.type.Naturwissenschaft, ((selection.ScienceType)(parent.NaturWissenschaftsIndex)).ToString());
                            data2.notDefined1 = new subjectTypes(subjectTypes.type.Fremdsprache, parent.FremdsprachenIndex);
                            data2.notDefined2 = new subjectTypes(parent.getExtras()[parent.Index2], parent.Index2);
                        }
                        else
                        {
                            data2 = new specialSubjectInfoUndefinedOrder();
                            data2.defined = new subjectTypes(subjectTypes.type.Fremdsprache, parent.FremdsprachenIndex);
                            data2.notDefined2 = new subjectTypes(subjectTypes.type.Naturwissenschaft, ((selection.ScienceType)(parent.NaturWissenschaftsIndex)).ToString());
                            data2.notDefined2 = new subjectTypes(parent.getExtras()[parent.Index2], parent.Index2);
                        }
                    }
                }
            }
        }

        public class sepecialSubjectInfoDefinedOrder
        {
            public subjectTypes science1, language1, extra;
            public bool continueExtra12;
        }
        public class specialSubjectInfoUndefinedOrder
        {
            public subjectTypes notDefined1, notDefined2, defined;
        }
    }
}
