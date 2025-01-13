const API_BASE_URL = 'http://localhost:5229/api/document';

export const documentService = {
    async uploadDocument(file, tags) {
        const base64 = await convertFileToBase64(file);
        const document = {
            fileName: file.name,
            dataInBase64: base64,
            tags: tags
        };

        const response = await fetch(`${API_BASE_URL}/upload`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(document)
        });

        if (!response.ok) {
            throw new Error('Failed to upload document');
        }
    },

    async getDocuments(tags = []) {
        const queryParams = tags.length > 0 
            ? `?${tags.map(tag => `tags=${encodeURIComponent(tag)}`).join('&')}` 
            : '';
            
        const response = await fetch(`${API_BASE_URL}${queryParams}`, {
            headers: {
                'Accept': 'application/json'
            }
        });
        
        if (!response.ok) {
            throw new Error('Failed to fetch documents');
        }

        return response.json();
    },

    async deleteDocument(documentId) {
        const response = await fetch(`${API_BASE_URL}/delete`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(documentId)
        });

        if (!response.ok) {
            throw new Error('Failed to delete document');
        }
    }
};

const convertFileToBase64 = (file) => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            const base64String = reader.result.split(',')[1];
            resolve(base64String);
        };
        reader.onerror = (error) => reject(error);
    });
};
