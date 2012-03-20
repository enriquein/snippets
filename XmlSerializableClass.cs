public class XmlSerializableClass<T> where T : new()
{
    protected void Save(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            xs.Serialize(fs, this);
        }
    }

    protected static T Load(string filePath)
    {
        T objectToReturn;
        FileStream fs = null;

        try
        {
            fs = new FileStream(filePath, FileMode.Open);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            objectToReturn = (T)xs.Deserialize(fs);
        }
        catch (DirectoryNotFoundException)
        {
            objectToReturn = new T();
        }
        catch (FileNotFoundException)
        {
            objectToReturn = new T();
        }
        finally
        {
            if (fs != null)
                fs.Close();
        }

        return objectToReturn;
    }
}