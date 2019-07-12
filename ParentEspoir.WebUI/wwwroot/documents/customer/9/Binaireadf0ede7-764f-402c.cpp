#include <algorithm>
#include <bitset>
#include <math.h>
#include "Binaire.h"


// *********************************************************************************
// Description : Donne le nombre de bits d'un entier base sur le bit à 1 avec la
//				 plus grande valeur.
//
// PRECONDITION : La valeur doit être positive
// 
// Retourne : Pointeur de bool
// *********************************************************************************
int TrouverLeNombreDeBits(int p_Entier) {
	int plusGrandBit = 0;
	std::bitset<32> x(p_Entier);
	for (unsigned int i = 0; i < x.size(); i++)
		if (x[i]) plusGrandBit = i + 1;
	return plusGrandBit;
}

// *********************************************************************************
// Description : Creer une chaine binaire avec un entier donne.
//
// PRECONDITION : La valeur doit être positive
// 
// Retourne : Pointeur de bool
// *********************************************************************************
bool * CreerUneChaineBinaire(int p_Entier) {
	bool * chaineBinaire = NULL;
	int nombreDeBits = TrouverLeNombreDeBits(p_Entier);
	if (nombreDeBits) {
		chaineBinaire = new bool[nombreDeBits];
		for (int puissance = nombreDeBits - 1; puissance >= 0; puissance--) {
			if (p_Entier - pow(2, puissance) >= 0) {
				p_Entier -= (int)pow(2, puissance);
				chaineBinaire[puissance] = 1;
			}
			else chaineBinaire[puissance] = 0;
		}
	}
	return chaineBinaire;
}

// *********************************************************************************
// Description : Creer une chaine binaire de la partie decimale d'un nombre.
//
// 
// Retourne : Pointeur sur une structure s_Fractionnaire
// *********************************************************************************
s_Fractionnaire* CreerUneChaineBinaireFractionnaire(double p_Double) {
	s_Fractionnaire * s_Decimal = new s_Fractionnaire;
	s_Decimal->m_MemoiresAllouees = 8;
	s_Decimal->m_NombreDeBits = 0;
	s_Decimal->m_Bits = new bool[s_Decimal->m_MemoiresAllouees];

	for (int i = 0; i < s_Decimal->m_MemoiresAllouees && p_Double != 1.0 && p_Double != 0.0; i++) {
		s_Decimal->m_NombreDeBits++;
		if (p_Double > 1.0) p_Double = p_Double - (int)p_Double;
		p_Double *= 2;

		if (p_Double >= 1) s_Decimal->m_Bits[i] = 1;
		else s_Decimal->m_Bits[i] = 0;

		if (i == s_Decimal->m_MemoiresAllouees - 1 && i != 31) {
			bool * temporaire = new bool[s_Decimal->m_MemoiresAllouees];
			for (int i = 0; i < s_Decimal->m_MemoiresAllouees; i++)
				temporaire[i] = s_Decimal->m_Bits[i];

			delete[] s_Decimal->m_Bits;
			s_Decimal->m_Bits = NULL;
			s_Decimal->m_MemoiresAllouees += 8;
			s_Decimal->m_Bits = new bool[s_Decimal->m_MemoiresAllouees];

			for (int i = 0; i <= s_Decimal->m_NombreDeBits; i++)
				s_Decimal->m_Bits[i] = temporaire[i];

			delete[] temporaire;
		}
	}
	return s_Decimal;
}

Binaire::Binaire() {
	this->m_EstNegatif = false;
	this->m_NombreDeBitsPartieEntiere = 0;
	this->m_BitsPartieEntiere = NULL;
	this->m_Fractionnaire = NULL;
}

Binaire::Binaire(int p_Entier) {
	*this = p_Entier;
}

Binaire::Binaire(double p_Double) {
	*this = p_Double;
}

Binaire::Binaire(const Binaire & p_Valeur) {
	*this = p_Valeur;
}

Binaire::~Binaire() {
	this->Vider();
}

void Binaire::Vider() {
	this->m_EstNegatif = false;
	this->m_NombreDeBitsPartieEntiere = 0;
	delete[] this->m_BitsPartieEntiere;
	this->m_BitsPartieEntiere = NULL;
	if (this->m_Fractionnaire != NULL) {
		delete[] this->m_Fractionnaire->m_Bits;
		delete this->m_Fractionnaire;
		this->m_Fractionnaire = NULL;
	}
}

bool Binaire::GetEstNegatif() const {
	return this->m_EstNegatif;
}

int Binaire::GetNombreDeBitsPartieEntiere() const {
	return this->m_NombreDeBitsPartieEntiere;
}

bool * Binaire::GetBitsPartieEntere() const {
	return this->m_BitsPartieEntiere;
}

int Binaire::GetNombreDeBitsPartieFractionnaire() const {
	return this->m_Fractionnaire->m_NombreDeBits;
}

bool * Binaire::GetBitsPartieFractionnaire() const {
	return this->m_Fractionnaire->m_Bits;
}

void Binaire::operator=(int p_Entier) {
	if (!this->EstVide() && this->m_NombreDeBitsPartieEntiere >= 0) this->Vider();
	if (p_Entier < 0) this->m_EstNegatif = true; else this->m_EstNegatif = false;
	this->m_NombreDeBitsPartieEntiere = TrouverLeNombreDeBits(abs(p_Entier));
	this->m_BitsPartieEntiere = CreerUneChaineBinaire(abs(p_Entier));
	this->m_Fractionnaire = NULL;
}

void Binaire::operator=(double p_Double) {
	if (!this->EstVide() && this->m_NombreDeBitsPartieEntiere >= 0) this->Vider();
	if (p_Double < 0) this->m_EstNegatif = true; else this->m_EstNegatif = false;
	this->m_NombreDeBitsPartieEntiere = TrouverLeNombreDeBits(abs((int)p_Double));
	this->m_BitsPartieEntiere = CreerUneChaineBinaire(abs((int)p_Double));
	this->m_Fractionnaire = CreerUneChaineBinaireFractionnaire(abs(p_Double));
}

void Binaire::operator=(const Binaire & p_Valeur) {
	if (!this->EstVide() && this->m_NombreDeBitsPartieEntiere >= 0) this->Vider();
	this->m_EstNegatif = p_Valeur.m_EstNegatif;
	this->m_NombreDeBitsPartieEntiere = p_Valeur.m_NombreDeBitsPartieEntiere;
	if (p_Valeur.m_BitsPartieEntiere != NULL) {
		this->m_BitsPartieEntiere = new bool[this->m_NombreDeBitsPartieEntiere];
		for (int i = 0; i < this->m_NombreDeBitsPartieEntiere; i++)
			this->m_BitsPartieEntiere[i] = p_Valeur.m_BitsPartieEntiere[i];
	}
	else this->m_BitsPartieEntiere = NULL;
	if (p_Valeur.m_Fractionnaire != NULL) {
		this->CopierLaPartieDecimale(p_Valeur);
	}
	else this->m_Fractionnaire = NULL;
}

Binaire Binaire::operator!() const {
	Binaire inverse = MasquePartieEntiere(31);
	//inverse.m_EstNegatif = !this->m_EstNegatif;
	for (int i = this->m_NombreDeBitsPartieEntiere - 1; i >= 0; i--)
		inverse.m_BitsPartieEntiere[i] = !this->m_BitsPartieEntiere[i];

	inverse.SupprimerLesZerosAuDebutDUnePartieEntiere();

	if (this->m_Fractionnaire != NULL) {
		inverse = inverse | MasqueDecimale(32);
		for (int i = 0; i < this->m_Fractionnaire->m_NombreDeBits; i++)
			inverse.m_Fractionnaire->m_Bits[i] = !this->m_Fractionnaire->m_Bits[i];
		inverse.SupprimerLesZerosALaFinDUnePartieDecimale();
	}
	
	return inverse;
}

bool Binaire::operator==(const Binaire & p_Valeur) const{
	bool estEgual = this->m_EstNegatif == p_Valeur.m_EstNegatif;
	if (this->m_NombreDeBitsPartieEntiere == p_Valeur.m_NombreDeBitsPartieEntiere)
		for (int i = 0; i < this->m_NombreDeBitsPartieEntiere && estEgual; i++)
			estEgual = this->m_BitsPartieEntiere[i] == p_Valeur.m_BitsPartieEntiere[i];
	else estEgual = false;

	if (estEgual && this->m_Fractionnaire != NULL && p_Valeur.m_Fractionnaire != NULL) {
		if (this->m_Fractionnaire->m_NombreDeBits == p_Valeur.m_Fractionnaire->m_NombreDeBits)
			for (int i = 0; i < this->m_Fractionnaire->m_NombreDeBits && estEgual; i++)
				estEgual = this->m_Fractionnaire->m_Bits[i] == p_Valeur.m_Fractionnaire->m_Bits[i];
		else estEgual = false;
	}
	return estEgual;
}

bool Binaire::operator!=(const Binaire & p_Valeur) const {
	return !(*this == p_Valeur);
}

bool Binaire::operator<(const Binaire & p_Valeur) const {
	bool estPlusPetit = false;
	if (p_Valeur.m_Fractionnaire != NULL || this->m_Fractionnaire != NULL)
		estPlusPetit = this->ToDouble() < p_Valeur.ToDouble();
	else
		estPlusPetit = this->ToInt() < p_Valeur.ToInt();
	return estPlusPetit;
}

bool Binaire::operator<=(const Binaire & p_Valeur) const {
	bool estPlusPetit = false;
	if (p_Valeur.m_Fractionnaire != NULL || this->m_Fractionnaire != NULL)
		estPlusPetit = this->ToDouble() <= p_Valeur.ToDouble();
	else
		estPlusPetit = this->ToInt() <= p_Valeur.ToInt();
	return estPlusPetit;
}

bool Binaire::operator>(const Binaire & p_Valeur) const {
	bool estPlusPetit = false;
	if (p_Valeur.m_Fractionnaire != NULL || this->m_Fractionnaire != NULL)
		estPlusPetit = this->ToDouble() > p_Valeur.ToDouble();
	else
		estPlusPetit = this->ToInt() > p_Valeur.ToInt();
	return estPlusPetit;
}

bool Binaire::operator>=(const Binaire & p_Valeur) const {
	bool estPlusPetit = false;
	if (p_Valeur.m_Fractionnaire != NULL || this->m_Fractionnaire != NULL)
		estPlusPetit = this->ToDouble() >= p_Valeur.ToDouble();
	else
		estPlusPetit = this->ToInt() >= p_Valeur.ToInt();
	return estPlusPetit;
}

Binaire Binaire::operator&(const Binaire & p_Valeur) const {
	Binaire chaineEt(this->ToInt() & p_Valeur.ToInt());
	if (this->m_Fractionnaire != NULL && p_Valeur.m_Fractionnaire != NULL) {
		chaineEt.m_Fractionnaire = new s_Fractionnaire;
		int minBit = std::min(this->m_Fractionnaire->m_NombreDeBits, p_Valeur.m_Fractionnaire->m_NombreDeBits);
		chaineEt.m_Fractionnaire->m_MemoiresAllouees = minBit;
		chaineEt.m_Fractionnaire->m_Bits = new bool[minBit];
		chaineEt.m_Fractionnaire->m_NombreDeBits = 0;
		for (int i = 0; i < minBit; i++) {
			chaineEt.m_Fractionnaire->m_Bits[i] = this->m_Fractionnaire->m_Bits[i] & p_Valeur.m_Fractionnaire->m_Bits[i];
			chaineEt.m_Fractionnaire->m_NombreDeBits++;
		}
	}
	chaineEt.SupprimerLesZerosAuDebutDUnePartieEntiere();
	return chaineEt;
}

Binaire Binaire::operator|(const Binaire & p_Valeur) const {
	Binaire chaineOu(this->ToInt() | p_Valeur.ToInt());
	if (this->m_Fractionnaire != NULL && p_Valeur.m_Fractionnaire != NULL) {
		chaineOu.m_Fractionnaire = new s_Fractionnaire;
		int minBit = std::min(this->m_Fractionnaire->m_NombreDeBits, p_Valeur.m_Fractionnaire->m_NombreDeBits);
		int maxBit = std::max(this->m_Fractionnaire->m_NombreDeBits, p_Valeur.m_Fractionnaire->m_NombreDeBits);
		chaineOu.m_Fractionnaire->m_MemoiresAllouees = maxBit;
		chaineOu.m_Fractionnaire->m_Bits = new bool[maxBit];
		chaineOu.m_Fractionnaire->m_NombreDeBits = 0;
		for (int i = 0; i < minBit; i++) {
			chaineOu.m_Fractionnaire->m_Bits[i] = this->m_Fractionnaire->m_Bits[i] | p_Valeur.m_Fractionnaire->m_Bits[i];
			chaineOu.m_Fractionnaire->m_NombreDeBits++;
		}
		for (int i = minBit; i < maxBit; i++) {
			if (this->m_Fractionnaire->m_NombreDeBits > p_Valeur.m_Fractionnaire->m_NombreDeBits)
				chaineOu.m_Fractionnaire->m_Bits[i] = this->m_Fractionnaire->m_Bits[i];
			else chaineOu.m_Fractionnaire->m_Bits[i] = p_Valeur.m_Fractionnaire->m_Bits[i];
			chaineOu.m_Fractionnaire->m_NombreDeBits++;
		}
		chaineOu.SupprimerLesZerosALaFinDUnePartieDecimale();
	}
	else if (this->m_Fractionnaire != NULL || p_Valeur.m_Fractionnaire != NULL) {
		if (this->m_Fractionnaire != NULL) chaineOu.CopierLaPartieDecimale(*this);
		else chaineOu.CopierLaPartieDecimale(p_Valeur);
	}
	return chaineOu;
}

std::ostream& operator<<(std::ostream& p_Flux, const Binaire & p_Valeur) {
	if (p_Valeur.m_EstNegatif) p_Flux << "-";

	if (p_Valeur.m_NombreDeBitsPartieEntiere == 0) p_Flux << 0;
	for (int i = p_Valeur.m_NombreDeBitsPartieEntiere - 1; i >= 0; i--)
		p_Flux << p_Valeur.m_BitsPartieEntiere[i];

	if (p_Valeur.m_Fractionnaire != NULL) {
		p_Flux << ".";
		if (p_Valeur.m_Fractionnaire->m_NombreDeBits == 0) p_Flux << 0;
		for (int i = 0; i < p_Valeur.m_Fractionnaire->m_NombreDeBits; i++)
			p_Flux << p_Valeur.m_Fractionnaire->m_Bits[i];
	}

	return p_Flux;
}

int Binaire::ToInt() const {
	int unEntier = 0;
	for (int i = this->m_NombreDeBitsPartieEntiere - 1; i >= 0; i--)
		unEntier += this->m_BitsPartieEntiere[i] * (int)pow(2, i);
	if (this->m_EstNegatif) unEntier *= -1;
	return unEntier;
}

double Binaire::ToDouble() const {
	double unDouble = 0.0;
	unDouble = this->ToInt();
	if (this->m_Fractionnaire != NULL)
		for (int i = 0; i < this->m_Fractionnaire->m_NombreDeBits; i++)
			unDouble += this->m_Fractionnaire->m_Bits[i] * pow(2, (-1*(i+1)));
	if (this->m_EstNegatif) unDouble *= -1;
	return unDouble;
}

bool Binaire::EstVide() {
	return (this->m_BitsPartieEntiere == NULL) && (this->m_NombreDeBitsPartieEntiere == 0)
			&& (this->m_Fractionnaire == NULL);
}

bool Binaire::CheckInvariants() {
	return (this->m_BitsPartieEntiere == NULL) == (this->m_NombreDeBitsPartieEntiere == 0)
			&& this->m_NombreDeBitsPartieEntiere >= 0;
}

// *****************************************************************************************
// Description : Copie la partie decimale du nombre. Va allouee de la nouvelle memoire
//				 et ne liberera pas la memoire si elle etait dejà allouee. Attention
//				 avant d'utiliser cette methode.
//
// *****************************************************************************************
void Binaire::CopierLaPartieDecimale(const Binaire & p_Source) {
	this->m_Fractionnaire = new s_Fractionnaire;
	this->m_Fractionnaire->m_MemoiresAllouees = p_Source.m_Fractionnaire->m_MemoiresAllouees;
	this->m_Fractionnaire->m_NombreDeBits = p_Source.m_Fractionnaire->m_NombreDeBits;
	this->m_Fractionnaire->m_Bits = new bool[this->m_Fractionnaire->m_MemoiresAllouees];
	for (int i = 0; i < this->m_Fractionnaire->m_NombreDeBits; i++)
		this->m_Fractionnaire->m_Bits[i] = p_Source.m_Fractionnaire->m_Bits[i];
}

void Binaire::SupprimerLesZerosAuDebutDUnePartieEntiere() {
	if (this->m_NombreDeBitsPartieEntiere > 1)
		for (int i = this->m_NombreDeBitsPartieEntiere - 1; this->m_BitsPartieEntiere[i] == 0 && i > 0; i--)
			this->m_NombreDeBitsPartieEntiere--;
}

void Binaire::SupprimerLesZerosALaFinDUnePartieDecimale() {
	for (int i = this->m_Fractionnaire->m_NombreDeBits - 1; this->m_Fractionnaire->m_Bits[i] == 0 && i != 0; i--)
		this->m_Fractionnaire->m_NombreDeBits--;
}

Binaire Masque(int p_NombreDeBitsA1) {
	Binaire leMasque;
	if (p_NombreDeBitsA1 <= 31)
		leMasque = MasquePartieEntiere(p_NombreDeBitsA1);
	else if (p_NombreDeBitsA1 > 31)
		leMasque = MasquePartieEntiere(31) | MasqueDecimale(p_NombreDeBitsA1 - 31);
	return leMasque;
}

Binaire MasquePartieEntiere(int p_NombreDeBitsA1) {
	int valeurDuMasque = 0;
	for (int i = 0; i < p_NombreDeBitsA1 && i < 32; i++)
		valeurDuMasque += (int)pow(2, 30 - i);
	// return Binaire(-1 * valeurDuMasque);
	return Binaire(valeurDuMasque);
}

Binaire MasqueDecimale(int p_NombreDeBitsA1) {
	p_NombreDeBitsA1 *= -1;
	double valeurDuMasque = 0.0;
	for (int i = -1; i >= p_NombreDeBitsA1; i--)
		valeurDuMasque += pow(2, i);
	return Binaire(valeurDuMasque);
}