using Npgsql;
using System.Data;

namespace stock.Models;

class Mouvement{
    String Idmouvement;
    String Date;
    String Idarticle;
    double Quantiteentree;
    double Quantitesortie;
    double Prixunitaire;
    String Idmagasin;
    double Reste;

    public Mouvement(){}

    public Mouvement(String idmouvement,String date,String idarticle,double quantiteentree,double quantitesortie,double prixunitaire,String idmagasin,double reste){
        this.Idmouvement = idmouvement;
        this.Date = date;
        this.Idarticle = idarticle;
        this.Quantiteentree = quantiteentree;
        this.Quantitesortie = quantitesortie;
        this.Prixunitaire = prixunitaire;
        this.Idmagasin = idmagasin;
        this.Reste = reste;
    }

    public Mouvement(String date,String idarticle,double quantiteentree,double quantitesortie,double prixunitaire,String idmagasin){
        this.Date = date;
        this.Idarticle = idarticle;
        this.Quantiteentree = quantiteentree;
        this.Quantitesortie = quantitesortie;
        this.Prixunitaire = prixunitaire;
        this.Idmagasin = idmagasin;
    }



    public String getIdmouvement(){
        return this.Idmouvement;
    }

    public void setIdmouvement(String idmouvement){
        this.Idmouvement = idmouvement;
    }

    public String getDate(){
        return this.Date;
    }

    public void setDate(String date){
        this.Date = date;
    }

    public String getIdarticle(){
        return this.Idarticle;
    }

    public void setIdarticle(String idarticle){
        this.Idarticle = idarticle;
    }

    public double getQuantiteentree(){
        return this.Quantiteentree;
    }

    public void setQuantiteentree(double quantiteentree){
        this.Quantiteentree = quantiteentree;
    }

    public double getQuantitesortie(){
        return this.Quantitesortie;
    }

    public void setQuantitesortie(double quantitesortie){
        this.Quantitesortie = quantitesortie;
    }

    public double getPrixunitaire(){
        return this.Prixunitaire;
    }

    public void setPrixunitaire(double prixunitaire){
        this.Prixunitaire = prixunitaire;
    }

    public String getIdmagasin(){
        return this.Idmagasin;
    }

    public void setIdmagasin(String idmagasin){
        this.Idmagasin = idmagasin;
    }

    public double getReste(){
        return this.Reste;
    }

    public void setReste(double reste){
        this.Reste = reste;
    }

    public void insertionmouvement(NpgsqlConnection liaisonbase){
        String sql = "INSERT INTO mouvement(date,idarticle,quantiteentree,quantitesortie) VALUES (@date,@idarticle,@quantiteentree,@quantitesortie,@prixunitaire) RETURNING idmouvement";
        if(liaisonbase == null || liaisonbase.State == ConnectionState.Closed){
            Connexion connexion = new Connexion ();
            liaisonbase = connexion.createLiaisonBase();
            liaisonbase.Open();
        }
        try{
            NpgsqlCommand cmd = new NpgsqlCommand(sql, liaisonbase);
            cmd.Parameters.AddWithValue("@date", DateTime.Parse(this.getDate()));
            cmd.Parameters.AddWithValue("@idarticle", this.getIdarticle());
            cmd.Parameters.AddWithValue("@quantiteentree",this.getQuantiteentree());
            cmd.Parameters.AddWithValue("@quantitesortie",this.getQuantitesortie());
            String insertedId = cmd.ExecuteScalar().ToString();
            this.setIdmouvement(insertedId);
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }finally{
            if(liaisonbase != null){
                liaisonbase.Close();
            }
        }
    }



    public void insertionreste(NpgsqlConnection liaisonbase){
        String sql = "INSERT INTO reste VALUES (@idmouvement,@date,@reste)";
        if(liaisonbase == null || liaisonbase.State == ConnectionState.Closed){
            Connexion connexion = new Connexion ();
            liaisonbase = connexion.createLiaisonBase();
            liaisonbase.Open();
        }
        try{
            NpgsqlCommand cmd = new NpgsqlCommand(sql, liaisonbase);
            cmd.Parameters.AddWithValue("@idmouvement", this.getIdmouvement());
            cmd.Parameters.AddWithValue("@date", DateTime.Parse(this.getDate()));
            cmd.Parameters.AddWithValue("@reste", this.getReste());
            cmd.ExecuteNonQuery();
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }finally{
            if(liaisonbase != null){
                liaisonbase.Close();
            }
        }
    }


    public void todoentree(NpgsqlConnection liaisonbase){
        try{
            this.insertionmouvement(liaisonbase);
            this.setReste(this.getQuantiteentree());
            this.insertionreste(liaisonbase);
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }finally{
            if(liaisonbase != null){
                liaisonbase.Close();
            }
        }
    }
}