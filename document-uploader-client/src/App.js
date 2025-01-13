import React, { useState } from 'react';
import { DocumentUpload } from './components/DocumentUpload';
import { DocumentList } from './components/DocumentList';
import './App.css';

function App() {
  const [refreshTrigger, setRefreshTrigger] = useState(0);

  const handleUploadComplete = () => {
    setRefreshTrigger(prev => prev + 1);
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Document Uploader</h1>
      </header>
      <main>
        <DocumentUpload onUploadComplete={handleUploadComplete} />
        <DocumentList refreshTrigger={refreshTrigger} />
      </main>
    </div>
  );
}

export default App;
