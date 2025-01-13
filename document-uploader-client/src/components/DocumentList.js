import React, { useState, useEffect } from 'react';
import { documentService } from '../services/documentService';

export const DocumentList = ({ refreshTrigger }) => {
    const [documents, setDocuments] = useState([]);
    const [filterTags, setFilterTags] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const loadDocuments = async () => {
        try {
            setLoading(true);
            setError('');
            const tags = filterTags
                .split(',')
                .map(tag => tag.trim())
                .filter(tag => tag.length > 0);
            const docs = await documentService.getDocuments(tags);
            
            console.log("docs");
            console.log(docs);

            setDocuments(docs);
        } catch (err) {
            setError('Failed to load documents: ' + err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadDocuments();
    }, [filterTags, refreshTrigger]);

    const handleDelete = async (documentId) => {
        try {
            await documentService.deleteDocument(documentId);
            loadDocuments();
        } catch (err) {
            setError('Failed to delete document: ' + err.message);
        }
    };

    return (
        <div className="document-list">
            <h2>Documents</h2>
            <div className="filter-section">
                <label>
                    Filter by tags:
                    <input
                        type="text"
                        value={filterTags}
                        onChange={(e) => setFilterTags(e.target.value)}
                        placeholder="tag1, tag2, tag3"
                    />
                </label>
            </div>
            {error && <div className="error">{error}</div>}
            {loading ? (
                <div>Loading...</div>
            ) : (
                <div className="documents">
                    {documents.length === 0 ? (
                        <p>No documents found</p>
                    ) : (
                        documents.map((doc) => (
                            <div key={doc.id} className="document-item">
                                <span className="filename">{doc.fileName}</span>
                                <div className="tags">
                                    {doc.tags.map((tag, index) => (
                                        <span key={index} className="tag">
                                            {tag}
                                        </span>
                                    ))}
                                </div>
                                <button
                                    onClick={() => handleDelete(doc.id)}
                                    className="delete-btn"
                                >
                                    Delete
                                </button>
                            </div>
                        ))
                    )}
                </div>
            )}
        </div>
    );
};
