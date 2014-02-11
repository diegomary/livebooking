using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using  LivebookingTest.Models;
using System.IO;
using System.Xml.Serialization;

namespace LivebookingTest.Database
{
public class DinerDetailsRepository
{
    ObservableCollection<DinerDetails> _dinerdetailsrep;
    public DinerDetailsRepository()
    {       
      _dinerdetailsrep = new ObservableCollection<DinerDetails>();
      GetDataFromXmlDb();         
    }

    public ObservableCollection<DinerDetails> DinerDetailsRep { get { return _dinerdetailsrep; } }
   
    public void AddDinerDetail(DinerDetails _dnd)
    {
        _dinerdetailsrep.Add(_dnd);
        PersistDataToXmlDb();    
    }

    public void UpdateDinerDetail()
    {       
        PersistDataToXmlDb();
    }

    public void RemoveDinerDetail(DinerDetails _dnd)
    {
        _dinerdetailsrep.Add(_dnd);
        PersistDataToXmlDb();
    }

    private void PersistDataToXmlDb()
    {
        using (StreamWriter myWriter = new StreamWriter(@"C:\Users\Public\Documents\DinersRep.xml", false))
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(ObservableCollection<DinerDetails>));
            mySerializer.Serialize(myWriter, _dinerdetailsrep);
            myWriter.Close();
        }    
    }

    private void GetDataFromXmlDb()
    {
        if (File.Exists(@"C:\Users\Public\Documents\DinersRep.xml") && new FileInfo(@"C:\Users\Public\Documents\DinersRep.xml").Length > 0)
        {
            using (StreamReader myReader = new StreamReader(@"C:\Users\Public\Documents\DinersRep.xml", false))
            {                
                XmlSerializer mySerializer = new XmlSerializer(typeof(ObservableCollection<DinerDetails>));
                _dinerdetailsrep = (ObservableCollection<DinerDetails>)mySerializer.Deserialize(myReader);
                myReader.Close();               
            }
        }
    }

}
}




