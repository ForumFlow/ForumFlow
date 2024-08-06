import React, { useState } from 'react';
import '../index.css';


function AddComment({ onSubmit, parentId }) {
    const [newCommentText, setNewCommentText] = useState("");
    const [isTextFieldVisible, setIsTextFieldVisible] = useState(false);
  
    const handleAddCommentClick = () => {
      setIsTextFieldVisible(true);
    };
  
    const handleSubmitComment = () => {
      if (newCommentText.trim() !== "") {
        onSubmit(newCommentText, parentId);
        setNewCommentText(""); // Clear the input field after adding the comment
        setIsTextFieldVisible(false); // Hide the text field after submitting the comment
      }
    };
  
    return (
      <div>
        {isTextFieldVisible ? (
          <div>
            <input 
              type="text" 
              value={newCommentText} 
              onChange={(e) => setNewCommentText(e.target.value)} 
              placeholder="Enter your comment"
            />
            <button className="btn btn-primary mb-3" onClick={handleSubmitComment}>Submit Comment</button>
          </div>
        ) : (
          <button className="btn btn-primary mb-3" onClick={handleAddCommentClick}>Add Comment</button>
        )}
      </div>
    );
  }
  export default AddComment;