export const DeleteUser = (props) => {
  const deleteUserFetch = async () => {
    const response = await fetch(
      `https://localhost:7043/api/StockUser/deleteuser/${props.StockUserId}`,
      {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
      }
    );


    if (!response.ok) {
      const badData = await response.json();
      throw new Error(data.message);
    }
  };

  const deleteClickHandler = () => {
    deleteUserFetch();
  };

  return (
    <label>
      <button type='button' onClick={deleteClickHandler}>
        Delete Account
      </button>
    </label>
  );
};
