using Npgsql;
using System.Data;
namespace stock.Models;

class Magasin{

    String Idmagasin;
    String Nommagasin;
    String Lieu;

    public Magasin(){}
    public Magasin(String idmagasin,String nommagasin,String lieu){
        this.Idmagasin = idmagasin;
        this.Nommagasin = nommagasin;
        this.Lieu = lieu; 
    }

    public String getIdmagasin(){
        return this.Idmagasin;
    }

    public void setIdmagasin(String idmagasin){
        this.Idmagasin = idmagasin;
    }

    public String getNommagasin(){
        return this.Nommagasin;
    }

    public void setNommagasin(String nommagasin){
        this.Nommagasin = nommagasin;
    }

    public String getLieu(){
        return this.Lieu;
    }

    public void setLieu(String lieu){
        this.Lieu = lieu; 
    } 

    public List<Magasin> getallmagasin(NpgsqlConnection liaisonbase){
        List<Magasin> listemagasin = new List<Magasin>();
        String sql = "SELECT  * FROM magasin";
        if(liaisonbase == null || liaisonbase.State == ConnectionState.Closed){
            Connexion connexion = new Connexion ();
            liaisonbase = connexion.createLiaisonBase();
            liaisonbase.Open();
        }
        try{
            NpgsqlCommand cmd = new NpgsqlCommand(sql, liaisonbase);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read()){
                Magasin magasin = new Magasin(reader.GetString(0),reader.GetString(1),reader.GetString(2));
                listemagasin.Add(magasin);
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }finally{
            if(liaisonbase != null){
                liaisonbase.Close();
            }
        }
        return listemagasin;
    }
}