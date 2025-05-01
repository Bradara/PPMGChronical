window.firebaseInterop = {
    // Auth methods
    registerUser: async function (email, password) {
        try {
            const result = await firebase.auth().createUserWithEmailAndPassword(email, password);
            return {
                success: true,
                user: {
                    uid: result.user.uid,
                    email: result.user.email
                }
            };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    signIn: async function (email, password) {
        try {
            const result = await firebase.auth().signInWithEmailAndPassword(email, password);
            return {
                success: true,
                user: {
                    uid: result.user.uid,
                    email: result.user.email
                }
            };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    signOut: async function () {
        try {
            await firebase.auth().signOut();
            return { success: true };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    getCurrentUser: function () {
        return new Promise((resolve) => {
            firebase.auth().onAuthStateChanged(user => {
                if (user) {
                    resolve({
                        isAuthenticated: true,
                        uid: user.uid,
                        email: user.email
                    });
                } else {
                    resolve({ isAuthenticated: false });
                }
            });
        });
    },

    // Firestore methods
    addDocument: async function (collectionName, document) {
        try {
            const docRef = await firebase.firestore().collection(collectionName).add(JSON.parse(document));
            return { success: true, id: docRef.id };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    getDocument: async function (collectionName, documentId) {
        try {
            const doc = await firebase.firestore().collection(collectionName).doc(documentId).get();
            if (doc.exists) {
                return { success: true, data: JSON.stringify(doc.data()), id: doc.id };
            } else {
                return { success: false, error: "Document not found" };
            }
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    getCollection: async function (collectionName) {
        try {
            const snapshot = await firebase.firestore().collection(collectionName).get();
            const docs = snapshot.docs.map(doc => {
                return {
                    id: doc.id,
                    ...doc.data()
                };
            });
            return { success: true, data: JSON.stringify(docs) };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    updateDocument: async function (collectionName, documentId, updatedData) {
        try {
            await firebase.firestore().collection(collectionName).doc(documentId).update(JSON.parse(updatedData));
            return { success: true };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    deleteDocument: async function (collectionName, documentId) {
        try {
            await firebase.firestore().collection(collectionName).doc(documentId).delete();
            return { success: true };
        } catch (error) {
            return { success: false, error: error.message };
        }
    }
};