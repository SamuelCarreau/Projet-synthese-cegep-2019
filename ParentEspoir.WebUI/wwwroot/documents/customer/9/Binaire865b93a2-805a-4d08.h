#pragma once
#include <ostream>

struct s_Fractionnaire {
	int m_MemoiresAllouees;
	int m_NombreDeBits;
	bool * m_Bits;
};

int TrouverLeNombreDeBits(int p_Entier);
bool * CreerUneChaineBinaire(int p_Entier);
s_Fractionnaire * CreerUneChaineBinaireFractionnaire(double p_Double);

// *****************************************************************************
// Description : Accepte des 'int' et des 'double' et les transformes en 
//				 chaine binaire.
//
//				 Si un double est passe en parametres, la partie entiere
//				 va être traite comme un 'int' alors, si le nombre ne peut
//				 pas être contenu dans un int il y aura de la perte.
//
//				 Pour la partie decimale, plusieurs valeurs ne peuvent être
//				 calculer exactement donc ce n'est qu'une approximation.
//				 La partie decimale contient 32 bits.
//
// Auteur : Frederic Jacques
// *****************************************************************************
class Binaire {
public:
	Binaire();
	Binaire(int p_Entier);
	Binaire(double p_Double);
	Binaire(const Binaire & p_Valeur);

	~Binaire();
	void Vider();

	bool GetEstNegatif() const;
	int GetNombreDeBitsPartieEntiere() const;
	bool * GetBitsPartieEntere() const;
	int GetNombreDeBitsPartieFractionnaire() const;
	bool * GetBitsPartieFractionnaire() const;

	void operator=(int p_Entier);
	void operator=(double p_Double);
	void operator=(const Binaire & p_Valeur);
	Binaire operator!() const;
	bool operator==(const Binaire & p_Valeur) const;
	bool operator!=(const Binaire & p_Valeur) const;
	bool operator<(const Binaire & p_Valeur) const;
	bool operator<=(const Binaire & p_Valeur) const;
	bool operator>(const Binaire & p_Valeur) const;
	bool operator>=(const Binaire & p_Valeur) const;
	Binaire operator&(const Binaire & p_Valeur) const;
	Binaire operator|(const Binaire & p_Valeur) const;

	friend std::ostream& operator<<(std::ostream& p_Flux, const Binaire & p_Valeur);

	int ToInt() const;
	double ToDouble() const;

private:
	bool CheckInvariants();
	bool EstVide();
	void CopierLaPartieDecimale(const Binaire & p_Source);
	void SupprimerLesZerosAuDebutDUnePartieEntiere();
	void SupprimerLesZerosALaFinDUnePartieDecimale();
	

	bool m_EstNegatif;
	int m_NombreDeBitsPartieEntiere;
	bool * m_BitsPartieEntiere;
	s_Fractionnaire * m_Fractionnaire;
};

// *********************************************************************************
// Description : Retourne un masque binaire. Les bits seront mis à 1 à partire
//				 de la plus grande puissance du nombre jusqu'à la plus petit.
//
// Exemple : Le parametres 31 donne la partie entiere completement remplis de 1.
//			 Plus grand que 31, la partie decimale commence à ce remplire.
//
// PRECONDITION : Le parametres doit être de 0 à 63.
// **********************************************************************************
Binaire Masque(int p_NombreDeBitsA1);
// *********************************************************************************
// Description : Retourne un masque binaire. Les bits seront mis à 1 à partire
//				 de la plus grande puissance.
//
// Exemple : Le parametres 31 donne la partie entiere complete.
//
// PRECONDITION : Le parametres doit être de 0 à 31.
// **********************************************************************************
Binaire MasquePartieEntiere(int p_NombreDeBitsA1);
// *********************************************************************************
// Description : Retourne un masque binaire de la partie decimale. Les bits seront
//				 mis à 1 à partire de 2^-1 jusqu'a 2^-32
//
// Exemple : Le parametres 32 donne la partie decimale complete.
//
// PRECONDITION : Le parametres doit être de 0 à 32.
// **********************************************************************************
Binaire MasqueDecimale(int p_NombreDeBitsA1);
