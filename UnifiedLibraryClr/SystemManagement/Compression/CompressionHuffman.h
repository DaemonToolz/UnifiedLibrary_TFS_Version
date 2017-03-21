#pragma once
#include <string>
#include <Windows.h>
#include <iostream>
#include <fstream>

#include <bitset>
#include <queue>
#include <deque>
#include <functional>

#include <array>

using namespace std;

// Code to Update:
// http://tcharles.developpez.com/Huffman/codecpp.php
// Replace String by bytes
// Removing all display

/*
namespace UnifiedLibraryClr {
	namespace SystemManagement {
		namespace Compression {
			typedef unsigned char byte;

			template<class T>
			class PtrComparer {
			private:
				T* ptr;

			public:
				inline explicit PtrComparer(T *p = NULL) : ptr(p) {}
				inline explicit PtrComparer(T &p) : ptr(&p) {}

				inline T * get() const { return ptr; }
				inline void set(T *p) { ptr = p; }
				inline void set(T &p) { ptr = &p; }

				friend bool operator<(const PtrComparer<T> &A, const PtrComparer<T> &B) {
					return *(A.ptr) < *(B.ptr);
				}

				friend bool operator>(const PtrComparer<T> &A, const PtrComparer<T> &B) {
					return *(A.ptr) > *(B.ptr);
				}

				friend bool operator<=(const PtrComparer<T> &A, const PtrComparer<T> &B) {
					return *(A.ptr) <= *(B.ptr);
				}

				friend bool operator>=(const PtrComparer<T> &A, const PtrComparer<T> &B) {
					return *(A.ptr) >= *(B.ptr);
				}

				friend bool operator==(const PtrComparer<T> &A, const PtrComparer<T> &B) {
					return *(A.ptr) == *(B.ptr);
				}

				friend std::ostream &operator<<(std::ostream &out, const PtrComparer<T> &B) {
					return out << *(B.ptr);
				}

				friend std::istream &operator >> (std::istream &in, const PtrComparer<T> &B) {
					return in >> *(B.ptr);
				}

			};

#pragma region Utility
			typedef enum {
				NO_ERR = -1,
				IO_FILE_NOT_EXISTING = 0,
				IO_READ_WRITE_ERROR,
				IO_WRITE_ERROR,
				IO_READ_ERROR,
				IO_READ_OPEN_ERROR,
				IO_WRITE_OPEN_ERROR,
				IO_EMPTY,
				UNKNOWN_SRC_ERR,
				NOT_ANALYZED

			} ErrCodes;

			typedef enum { 
				MUTE = 0, 
				IN_PROGRESS, 
				STEP, 
				TALKATIVE 
			} Dialog;

			typedef void(*MsgSender)(const std::string &);
#pragma endregion


#pragma region Nodes
			struct HuffmanNode{
				unsigned char Value;                    
				unsigned int  Frequency;                 
				string * lpsCode;                   
														 
				HuffmanNode * Left;
				HuffmanNode * Right;

				inline HuffmanNode(unsigned char Val = 0, unsigned int Freq = 0, std::string * lpsCodeStr = NULL, HuffmanNode * Fg = NULL,  HuffmanNode * Fd = NULL) :  Value(Val), Frequency(Freq), lpsCode(lpsCodeStr), Left(Fg), Right(Fd) {}

				inline ~HuffmanNode(){
					if (Left) delete Left;
					if (Right) delete Right;
				}

				void DetermineHuffmanEncoding(const std::string &sCode) const {
					if (lpsCode) *lpsCode = sCode;
					else {
						if (Left)  Left->DetermineHuffmanEncoding(sCode + "0");
						if (Right) Right->DetermineHuffmanEncoding(sCode + "1");
					}
				}

				void CreateLeaf(const vector<byte> &sCode, unsigned char valeur) {
					HuffmanNode** p;
					if (sCode[0] == '0') p = &Left;
					else				 p = &Right;

					if (sCode.size() > 1){
						if (!(*p))
							*p = new HuffmanNode();
						(*p)->CreateLeaf(vector<byte>(sCode.begin()+1,sCode.end()), valeur);
					}
					else {
						*p = new HuffmanNode(valeur);
						p = p;
					}
				}

				friend bool operator<(const HuffmanNode &A, const HuffmanNode &B);
				friend bool operator>(const HuffmanNode &A, const HuffmanNode &B);
				friend bool operator==(const HuffmanNode &A, const HuffmanNode &B);
			};
			typedef HuffmanNode * PNoeudHuffman;

			
			struct HuffmanStatistics{
				unsigned long int Frequency;
				vector<byte> sCode;

				HuffmanStatistics() : Frequency(0), sCode() {}
				HuffmanNode * CreateNode(unsigned char Value) {
					//HuffmanNode(Value, Frequency, *sCode, nullptr, nullptr);
				}

				vector<byte> operator+=(const byte &newByte) {
					sCode.push_back(newByte);
					return sCode;
				}

				byte* BytesToString() {
					return sCode.data();
				}
			};

#pragma endregion

			class HuffmannTree {
			private :
				bool isCompressed;
				string src, targ;
				string DIRECTORY_SEPARATOR = "/";

#pragma region Pointers and sizes
				unsigned short int elements;
				unsigned long int  realSize;
				unsigned long long int endHeader;
#pragma endregion

#pragma region Referring tree
				HuffmanNode*		Tree;
				HuffmanStatistics * Statistics;   
				Dialog				Level;        
				MsgSender			ProgressionDisplay;
				MsgSender			StepDisplay;
#pragma endregion

#pragma region Error
				unsigned int		ErrCode;                 													  
				string				sErrString;               
#pragma endregion								 


			protected:
				virtual bool WriteHeader() {
					std::ofstream fDst;
					unsigned long int ULongValue;
					unsigned short int UShortValue;
					unsigned char UCharValue;
					std::string sNomFichier;
					unsigned long int DernierSeparateur;
					char BufferProgression[10];


					if (src.length()){
						if (targ.length())
							fDst.open(targ.c_str(), std::ios::out | std::ios::binary);
						else
						{
							ErrCode = IO_FILE_NOT_EXISTING;
							sErrString = "Veuillez préciser le nom du fichier de destination";
							return false;
						}
					}
					else
					{
						ErrCode = IO_FILE_NOT_EXISTING;
						sErrString = "Veuillez préciser le nom du fichier source";
						return false;
					}

					if (fDst.is_open())
					{
						// Ecriture de la taille du fichier en octets
						ULongValue = InitialSize();
						fDst.write((char *)(&ULongValue), sizeof(unsigned long int));
						if (!fDst.good())
						{
							ErrCode = IO_WRITE_ERROR;
							sErrString = "Erreur lors de l'écriture de l'entête";
							return false;
						}

						// Ecriture du nom du fichier original sans le chemin
						DernierSeparateur = src.rfind(DIRECTORY_SEPARATOR);
						if (DernierSeparateur > src.length())
							// Il n'y a pas le chemin
							sNomFichier = src;
						else
							sNomFichier = src.substr(DernierSeparateur + 1);

						if (src.empty())
						{
							ErrCode = IO_FILE_NOT_EXISTING;
							sErrString = "Veuillez préciser le fichier d'entrée";
							return false;
						}


						// Ecriture de la taille du nom du fichier original
						UCharValue = sNomFichier.length();
						fDst.write((char *)(&UCharValue), sizeof(char));
						if (!fDst.good())
						{
							ErrCode = IO_WRITE_ERROR;
							sErrString = "Erreur lors de l'écriture de l'entête";
							return false;
						}

						fDst.write(sNomFichier.c_str(), UCharValue);
						if (!fDst.good())
						{
							ErrCode = IO_WRITE_ERROR;
							sErrString = "Erreur lors de l'écriture de l'entête";
							return false;
						}

						// Ecriture du nombre d'éléments différents
						fDst.write((char *)(&elements), sizeof(unsigned short int));
						if (!fDst.good())
						{
							ErrCode = IO_WRITE_ERROR;
							sErrString = "Erreur lors de l'écriture de l'entête";
							return false;
						}

						for (int i = 0; i < 256; i++)
						{
							// Si le caractère a une fréquence nulle on passe au suivant
							if (!Statistics[i].Frequency)
								continue;

							// Ecriture du code du caractère
							UCharValue = i;
							fDst.write((char *)(&UCharValue), sizeof(char));
							if (!fDst.good())
							{
								ErrCode = IO_WRITE_ERROR;
								sErrString = "Erreur lors de l'écriture de l'entête";
								return false;
							}

							// Ecriture du nombre de bits du caractère
							UCharValue = Statistics[i].sCode.length();
							fDst.write((char *)(&UCharValue), sizeof(char));
							if (!fDst.good())
							{
								ErrCode = IO_WRITE_ERROR;
								sErrString = "Erreur lors de l'écriture de l'entête";
								return false;
							}

							if (UCharValue <= 8)
							{
								// Ecriture du code dans un espace de 8 bits
								UCharValue = 0;
								for (int j = 0; j < Statistics[i].sCode.length(); j++)
									if (Statistics[i].sCode[j] == '1')
										UCharValue |= (0x01 << (7 - j));
								fDst.write((char *)(&UCharValue), sizeof(char));
								if (!fDst.good())
								{
									ErrCode = IO_WRITE_ERROR;
									sErrString = "Erreur lors de l'écriture de l'entête";
									return false;
								}
							}
							else
							{
								// Ecriture du code dans un espace de 16 bits
								UShortValue = 0;
								for (int j = 0; j < Statistics[i].sCode.length(); j++)
									if (Statistics[i].sCode[j] == '1')
										UShortValue |= (0x01 << (15 - j));
								fDst.write((char *)(&UShortValue), sizeof(unsigned short int));
								if (!fDst.good())
								{
									ErrCode = IO_WRITE_ERROR;
									sErrString = "Erreur lors de l'écriture de l'entête";
									return false;
								}
							}
							sprintf(BufferProgression, "%d%%", (((i * 99) / 256) + 1));
						}
						fDst.close();
						ErrCode = NO_ERR;
						return true;
					}
					else
					{
						ErrCode = IO_READ_ERROR;
						sErrString = "Impossible d'ouvrir le fichier de sortie en écriture";
						return false;
					}

				}

				virtual bool ReadHeader() {
					std::ifstream fSrc;
					unsigned short int UShortValue;
					unsigned char UCharValue;
					char * buffer;
					int Elt;
					int NbBits;
					char BufferProgression[10];

					if (src.length())
						fSrc.open(src.c_str(), std::ios::in | std::ios::binary);
					else{
						ErrCode = IO_FILE_NOT_EXISTING;
						sErrString = "Veuillez préciser le nom du fichier source";
						return false;
					}

					if (fSrc.is_open())
					{
						// réinitialisation des statistiques
						if (Statistics)
							delete[] Statistics;

						Statistics = new HuffmanStatistics[256];

						// Lecture de la taille du fichier en octets
						fSrc.read((char *)(&realSize), sizeof(unsigned long int));
						if (!fSrc.good())
						{
							ErrCode = IO_READ_ERROR;
							sErrString = "Erreur lors de la lecture de l'entête";
							return false;
						}

						// Lecture de la taille du nom du fichier original
						fSrc.read((char *)(&UCharValue), sizeof(char));
						if (!fSrc.good())
						{
							ErrCode = IO_READ_ERROR;
							sErrString = "Erreur lors de la lecture de l'entête";
							return false;
						}

						// Création du buffer pour la lecture du nom du fichier
						buffer = new char[UCharValue + 1];

						// Lecture du nom du fichier original
						fSrc.read(buffer, UCharValue);
						if (!fSrc.good())
						{
							ErrCode = IO_READ_ERROR;
							sErrString = "Erreur lors de la lecture de l'entête";
							return false;
						}

						// Zero terminal
						buffer[UCharValue] = 0;
						if (targ.empty())
							targ = buffer;

						// Lecture du nombre d'éléments différents
						fSrc.read((char *)(&elements), sizeof(unsigned short int));
						if (!fSrc.good())
						{
							ErrCode = IO_READ_ERROR;
							sErrString = "Erreur lors de la lecture de l'entête";
							return false;
						}

						for (int i = 0; i < elements; i++)
						{
							// Lecture du code du caractère
							fSrc.read((char *)(&UCharValue), sizeof(char));
							if (!fSrc.good())
							{
								ErrCode = IO_READ_ERROR;
								sErrString = "Erreur lors de la lecture de l'entête";
								return false;
							}
							Elt = UCharValue;

							// Lecture du nombre de bits du caractère
							fSrc.read((char *)(&UCharValue), sizeof(char));
							if (!fSrc.good())
							{
								ErrCode = IO_READ_ERROR;
								sErrString = "Erreur lors de la lecture de l'entête";
								return false;
							}
							NbBits = UCharValue;

							if (NbBits <= 8)
							{
								// Lecture du code dans un espace de 8 bits
								fSrc.read((char *)(&UCharValue), sizeof(char));
								if (!fSrc.good())
								{
									ErrCode = IO_READ_ERROR;
									sErrString = "Erreur lors de la lecture de l'entête";
									return false;
								}
								for (int j = 0; j < NbBits; j++)
									Statistics[Elt] += (UCharValue & (0x01 << (7 - j))) ?
									1 : 0;
							}
							else
							{
								// Lecture du code dans un espace de 16 bits
								fSrc.read((char *)(&UShortValue), sizeof(unsigned short int));
								if (!fSrc.good())
								{
									ErrCode = IO_READ_ERROR;
									sErrString = "Erreur lors de la lecture de l'entête";
									return false;
								}
								for (int j = 0; j < NbBits; j++)
									Statistics[Elt] += (UShortValue & (0x01 << (15 - j)))
									? 1 : 0;
							}

							sprintf(BufferProgression, "%d%%", (((Elt * 99) / 256) + 1));
						}

						endHeader = fSrc.tellg();
						fSrc.seekg(0, std::ios::beg);
						endHeader -= fSrc.tellg();
						fSrc.close();
						ErrCode = NO_ERR;
						return true;
					}
					else
					{
						ErrCode = IO_READ_OPEN_ERROR;
						sErrString = "Impossible d'ouvrir le fichier d'entrée en lecture";
						return false;
					}
				}


				bool BuildFrequencyTree() {
					typedef PtrComparer<HuffmanNode> PtrCompHuffman;
					PtrCompHuffman NoeudA;
					PtrCompHuffman NoeudB;
					// Création d'une file à priorité classée en ordre inverse
					priority_queue<PtrCompHuffman,
						deque<PtrCompHuffman>,
						greater<PtrCompHuffman> > FilePrioNoeuds;

					if (!Statistics)
					{
						ErrCode = NOT_ANALYZED;
						sErrString = "L'analyse n'a pas été effectuée";
						return false;
					}

					// Création de la liste initiale
					for (int i = 0; i < 256; i++)
						if (Statistics[i].Frequency)
						{
							// On ne considère que les fragments qui apparaissent
							FilePrioNoeuds.push(PtrCompHuffman(Statistics[i].CreateNode(i)));
						}

					if (FilePrioNoeuds.empty())
					{
						ErrCode = IO_EMPTY;
						sErrString = "Fichier vide";
						return false;
					}

					elements = FilePrioNoeuds.size();

					// Génération de l'arbre de Huffman
					while (FilePrioNoeuds.size() > 1)
					{
						NoeudA = FilePrioNoeuds.top();
						FilePrioNoeuds.pop();
						NoeudB = FilePrioNoeuds.top();
						FilePrioNoeuds.pop();

						FilePrioNoeuds.push(PtrCompHuffman(new HuffmanNode(0,
							NoeudA.get()->Frequency
							+ NoeudB.get()->Frequency,
							NULL,
							NoeudA.get(),
							NoeudB.get())));
					}

					// Détermination de la racine et vidage de la file à priorité
					Tree = FilePrioNoeuds.top().get();
					FilePrioNoeuds.pop();

					// Détermination des codes de Huffman
					Tree->DetermineHuffmanEncoding("");

					ErrCode = NO_ERR;
					return true;

				}

				
				bool BuildTree() {
					char BufferProg[10];
					// Création de la racine
					Tree = new HuffmanNode();
					for (int i = 0; i < 256; i++)
					{
						if (Statistics[i].sCode.size())
						{
							Tree->CreateLeaf(Statistics[i].sCode, i);
						}
						sprintf(BufferProg, "%d%%", (i * 100) / 256);
			
					}
					return true;
				}

			public:
				explicit HuffmannTree(const std::string &sSrc = std::string(""),
					const std::string &sDst = std::string(""),
					Dialog Dialogue = TALKATIVE,
					const MsgSender Progres = NULL,
					const MsgSender Etape = NULL);

				virtual ~HuffmannTree();


				virtual bool Analyze() {
					ifstream fSrc;
					char Valeur;
					char BufferProgression[10];
					unsigned long int Taille = InitialSize();
					unsigned long int Position = 0;

					// Si le nom du fichier n'est pas défini ce n'est pas nécessaire d'essayer
					// de l'ouvrir
					if (src.length())
						fSrc.open(src.c_str(), std::ios::in | std::ios::binary);
					else
					{
						ErrCode = IO_FILE_NOT_EXISTING;
						sErrString = "Veuillez préciser le nom du fichier source";
						return false;
					}

					if (fSrc.is_open())
					{
						// Allocation du tableau de statistiques
						if (Statistics)
							delete[] Statistics;
						Statistics = new HuffmanStatistics[256];

						// Parcours du fichier caractère par caractère
						// Les optimisations d'accès au fichier sont gérées par l'OS
						while (!fSrc.eof())
						{
							fSrc.read((char *)(&Valeur), sizeof(char));
							Statistics[(unsigned char)(Valeur)].Frequency++;
							sprintf(BufferProgression, "%d%%", int(float(Position) / Taille * 100));
							Position++;
						}

						// Fermeture du fichier
						fSrc.close();
						return true;
						ErrCode = NO_ERR;
					}
					else
					{
						ErrCode = IO_READ_WRITE_ERROR;
						sErrString = "Impossible d'ouvrir le fichier d'entrée en lecture";
						return false;
					}
				}

				bool Compress() {
					typedef bitset<32> TDataBuffer;
					std::ifstream fSrc;
					std::ofstream fDst;
					char Valeur;
					char BufferProg[10];
					unsigned long int Position = 0;
					std::string BufferOut;
					TDataBuffer Data;
					unsigned long int uData;

					realSize = InitialSize();

					if (!Statistics)
						if (!Analyze())
							return false;

					if (!Tree)
						if (!BuildFrequencyTree())
							return false;

					if (!WriteHeader())
						return false;

					// Compression du fichier source

					if (src.length()){
						// Ouverture du fichier source
						fSrc.open(src.c_str(), std::ios::in | std::ios::binary);

						// Ouverture du fichier de destination
						if (fSrc.is_open()){
							if (targ.length())
								fDst.open(targ.c_str(), std::ios::app | std::ios::binary);
							else
							{
								ErrCode = IO_FILE_NOT_EXISTING;
								sErrString = "Veuillez préciser le nom du fichier de destination";
								return false;
							}
						}
						else
						{
							ErrCode = IO_READ_WRITE_ERROR;
							sErrString = "Impossible d'ouvrir le fichier d'entrée en lecture";
							return false;
						}
					}
					else
					{
						ErrCode = IO_FILE_NOT_EXISTING;
						sErrString = "Veuillez préciser le nom du fichier source";
						return false;
					}

					if (fDst.is_open()){
						// Parcours du fichier caractère par caractère
						// Les optimisations d'accès au fichier sont gérées par l'OS
						fSrc.read((char *)(&Valeur), sizeof(char));
						while (!fSrc.eof()){
							// insertion du code correspondant dans le buffer
							BufferOut += Statistics[(unsigned char)(Valeur)].BytesToString();
							if (BufferOut.length() > 31)
							{
								// On dispose d'assez de données pour écrire
								// Conversion du buffer de caractères '1' et '0' en entier long
								Data = static_cast<TDataBuffer>(BufferOut.substr(0, 32));
								uData = Data.to_ulong();
								// écriture des données
								fDst.write((char *)(&uData), sizeof(unsigned long int));
								if (!fDst.good())
								{
									ErrCode = IO_READ_WRITE_ERROR;
									sErrString = "Erreur lors de l'écriture des données";
									return false;
								}
								// libération du buffer
								BufferOut.erase(0, 32);
							}
							sprintf(BufferProg, "%d%%", int(float(Position) / realSize * 100));
							Position++;
							fSrc.read((char *)(&Valeur), sizeof(char));
						}

						// vidage du reste du buffer
						Data.reset();
						BufferOut.resize(32, '0');
						Data = static_cast<TDataBuffer>(BufferOut);
						uData = Data.to_ulong();
						fDst.write((char *)(&uData), sizeof(unsigned long int));

						// Fermeture des fichier
						fSrc.close();
						fDst.close();
					}
					else
					{
						ErrCode = IO_WRITE_ERROR;
						sErrString = "Impossible d'ouvrir le fichier de sortie en écriture";
						return false;
					}

					ErrCode = NO_ERR;
					return true;
				}

				bool Decompress() {
					typedef std::bitset<32> TDataBuffer;
					std::ifstream fSrc;
					std::ofstream fDst;
					char Valeur;
					char BufferProg[10];
					unsigned long int Position = 0;
					TDataBuffer Data;
					unsigned long int uData;
					HuffmanNode * Pos;
					int i;


					if (!Statistics)
						if (!ReadHeader())
							return false;

					if (!Tree)
						if (!BuildTree())
							return false;

					// Décompression du fichier source

					if (src.length()){
						// Ouverture du fichier source
						fSrc.open(src.c_str(), std::ios::in | std::ios::binary);

						// Ouverture du fichier de destination
						if (fSrc.is_open())
						{
							if (targ.length())
								fDst.open(targ.c_str(), std::ios::out | std::ios::binary);
							else
							{
								ErrCode = IO_FILE_NOT_EXISTING;
								sErrString = "Veuillez préciser le nom du fichier de destination";
								return false;
							}
						}
						else
						{
							ErrCode = IO_READ_ERROR;
							sErrString = "Impossible d'ouvrir le fichier d'entrée en lecture";
							return false;
						}
					}
					else
					{
						ErrCode = IO_FILE_NOT_EXISTING;
						sErrString = "Veuillez préciser le nom du fichier source";
						return false;
					}

					if (fDst.is_open())
					{
						fSrc.seekg(endHeader, std::ios::beg);

						Pos = Tree;
						// Parcours du fichier 4 octets par 4 octets
						// Les optimisations d'accès au fichier sont gérées par l'OS
						fSrc.read((char *)(&uData), sizeof(unsigned long int));
						while (!fSrc.eof())
						{

							// Conversion du buffer en bitset
							Data = static_cast<TDataBuffer>(uData);

							// Parcours du buffer pour décoder les caractères
							for (i = 31; (i >= 0) && (Position < realSize); i--)
							{
								// Parcours de l'arbre
								if (Data.test(i))
									Pos = Pos->Right;
								else
									Pos = Pos->Left;

								if (!Pos)
								{
									ErrCode = UNKNOWN_SRC_ERR;
									sErrString = "Impossible de lire dans l'arbre de Huffman";
									return false;
								}

								if (!(Pos->Right || Pos->Left))
								{
									// Feuille terminale : écriture de l'octet
									fDst.write((char *)(&(Pos->Value)), 1);
									if (!fDst.good())
									{
										ErrCode = IO_WRITE_ERROR;
										sErrString = "Erreur lors de l'écriture des données";
										return false;
									}
									// retour à la racine de l'arbre
									Pos = Tree;
									Position++;
								}
							}

							sprintf(BufferProg, "%d%%", int(float(Position) / realSize * 100));
							fSrc.read((char *)(&uData), sizeof(unsigned long int));
						}

						// Fermeture des fichier
						fSrc.close();
						fDst.close();
					}
					else {
						ErrCode = IO_READ_ERROR;
						sErrString = "Impossible d'ouvrir le fichier de sortie en écriture";
						return false;
					}

					ErrCode = NO_ERR;
					return true;
				}

				unsigned long int InitialSize() {
					return src.length() ? ifstream(src, std::ifstream::ate | std::ifstream::binary).tellg() : 0;
				}

				unsigned long int CompressedSize() {
					return targ.length() ? ifstream(targ, std::ifstream::ate | std::ifstream::binary).tellg() : 0;
				}

				unsigned long int FinalSize() {
					return realSize;
				}

				void SetSource(const std::string &sSrc) {
					src = sSrc;
				}

				void SetTarget(const std::string &sDst) {
					targ = sDst;
				}

				void SetDialog(const Dialog v) {
					Level = v;
				}

				void SetProgressFunc(MsgSender Progres);
				void SetStepFunc(MsgSender Etape);

				string GetSource() const {
					return src;
				}

				string GetDestination() const {
					return targ;
				}

				Dialog GetDegreDialogue() const {
					return Level;
				}

				unsigned int GetLastErr(std::string &sMessage) const {
					throw new exception("Not Implemented.");
				}

				// Fonction par défaut pour l'affichage des messages.
				static void DisplayMessage(const std::string &sMessage);
			};


		}
	}
}
*/