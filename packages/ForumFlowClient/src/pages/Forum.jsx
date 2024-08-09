import React, { useState } from "react";
import Presentation from "../components/Presentation";
import Comment from "../components/comment";
import AddComment from "../components/AddComment";
import AddPresentation from "../components/AddPresentation";

export default function Forum(){
  const [comments, setComments] = useState([]);
  const [presentation, setPresentation] = useState(null);
  const [submitted, setSubmitted] = useState(false);

  const handleAddComment = (text, parentId = null) => {
    const newComment = {
      id: comments.length + 1, // Simple unique ID generation
      text,
      parentId,
      upvotes: 0,
      downvotes: 0,
    };
    setComments([...comments, newComment]);
  };

  const handleNewPresentation = (addPresentation) => {
    setPresentation(addPresentation);
    setSubmitted(true);
  };

  return (
    <div className="p-4 bg-gray-100 min-h-screen">
      {!submitted && (
        <div className="mb-4 p-4 bg-white shadow-md rounded">
          <AddPresentation onSubmit={handleNewPresentation} />
        </div>
      )}
      
      {presentation && (
        <div className="mb-4 p-4 bg-white shadow-md rounded">
          <Presentation
            author={presentation.author}
            text={presentation.text}
            title={presentation.title}
          />
        </div>
      )}
      {submitted && (
        <div className="mb-4 p-4 bg-white shadow-md rounded">
          <AddComment onSubmit={handleAddComment} parentId={null} />
        </div>
      )}
      {comments.map((comment) => (
        <div className="container mb-4 p-4 bg-white shadow-md rounded" key={comment.id}>
          <Comment
            text={comment.text}
            upvotes={comment.upvotes}
            downvotes={comment.downvotes}
            id={comment.id}
            parentId={comment.parentId}
          />
          <div className="mt-2">
            <AddComment onSubmit={handleAddComment} parentId={comment.id} />
          </div>
        </div>
      ))}
    </div>
  );
}