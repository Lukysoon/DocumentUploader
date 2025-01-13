import React, { useState } from 'react';
import { documentService } from '../services/documentService';

export const DocumentUpload = ({ onUploadComplete }) => {
    const [file, setFile] = useState(null);
    const [tags, setTags] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const handleFileChange = (e) => {
        if (e.target.files[0]) {
            setFile(e.target.files[0]);
            setError('');
        }
    };

    const handleTagsChange = (e) => {
        setTags(e.target.value);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!file) {
            setError('Please select a file');
            return;
        }

        try {
            setLoading(true);
            setError('');
            const tagList = tags.split(',')
                .map(tag => tag.trim())
                .filter(tag => tag.length > 0);

            await documentService.uploadDocument(file, tagList);
            setFile(null);
            setTags('');
            if (onUploadComplete) {
                onUploadComplete();
            }
        } catch (err) {
            setError('Failed to upload document: ' + err.message);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="upload-container">
            <h2>Upload Document</h2>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>
                        File:
                        <input
                            type="file"
                            onChange={handleFileChange}
                            disabled={loading}
                        />
                    </label>
                </div>
                <div className="form-group">
                    <label>
                        Tags (comma-separated):
                        <input
                            type="text"
                            value={tags}
                            onChange={handleTagsChange}
                            placeholder="tag1, tag2, tag3"
                            disabled={loading}
                        />
                    </label>
                </div>
                {error && <div className="error">{error}</div>}
                <button type="submit" disabled={loading}>
                    {loading ? 'Uploading...' : 'Upload'}
                </button>
            </form>
        </div>
    );
};
