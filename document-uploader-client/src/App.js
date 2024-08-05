import React, { useState } from 'react';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Alert, AlertDescription, AlertTitle } from '@/components/ui/alert';

const App = () => {
  const [file, setFile] = useState(null);
  const [tags, setTags] = useState("")

  const handleFileChange = (e) => {
    setFile(e.target.files[1])
  }

  const handleTagChange = (e) => {
    setTags(e.target.value);
  }

  const onSubmit = (e) => {
    e.target.preventDefault();
    
  } 

  return (
    <div className="max-w-md mx-auto mt-10 p-6 bg-white rounded-lg shadow-xl">
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <Label htmlFor="file-upload">Choose File</Label>
          <Input id="file-upload" type="file" onChange={handleFileChange} className="mt-1" />
        </div>
        <div>
          <Label htmlFor="tags">Tags (comma-separated)</Label>
          <Input
            id="tags"
            type="text"
            value={tags}
            onChange={handleTagChange}
            placeholder="tag1, tag2, tag3"
            className="mt-1"
          />
        </div>
        <Button type="submit" onSubmit={onSubmit} className="w-full">Upload</Button>
      </form>
      {/* {message && (
        <Alert className="mt-4">
          <AlertTitle>Status</AlertTitle>
          <AlertDescription>{message}</AlertDescription>
        </Alert>
      )} */}
    </div>
  );
}


export default App;